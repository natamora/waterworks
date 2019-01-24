using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Models.View.Client;
using Waterworks.Models;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Microsoft.EntityFrameworkCore;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;
using Newtonsoft.Json.Linq;
using Waterworks.Models.View.Object;
using X.PagedList;
using Waterworks.Models.View;
using Waterworks.Models.View.WaterMeter;

namespace Waterworks.Controllers
{
    public class ObjectController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ObjectController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult About(int objectId)
        {
            return PartialView();
        }
        public IActionResult Index(string sortOrder, string currentCategory, string category, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["HouseNumberSortParam"] = sortOrder == "HouseNumber" ? "HouseNumber_desc" : "HouseNumber";
            ViewData["StreetSortParam"] = sortOrder == "Street" ? "Street_desc" : "Street";
            ViewData["PostalCodeSortParam"] = sortOrder == "PostalCode" ? "PostalCode_desc" : "PostalCode";
            ViewData["ApartmentNumberSortParam"] = sortOrder == "ApartmentNumber" ? "ApartmentNumber_desc" : "ApartmentNumber";
            ViewData["PlotNumberSortParam"] = sortOrder == "PlotNumber" ? "PlotNumber_desc" : "PlotNumber";
            ViewData["PostSortParam"] = sortOrder == "Post" ? "Post_desc" : "Post";
            ViewData["PayoffSortParam"] = sortOrder == "Payoff" ? "Payoff_desc" : "Payoff";
            ViewData["CitySortParam"] = sortOrder == "City" ? "City_desc" : "City";
            ViewData["ClientIdSortParam"] = sortOrder == "ClientId" ? "ClientId_desc" : "ClientId";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                category = currentCategory;
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCategory"] = category;
            var objects = dbContext.Obiekt
                .Include(o => o.AdresObiektu)
                .ToList();

            List<BasicCreateObjectViewModel> list = new List<BasicCreateObjectViewModel>();

            if (!String.IsNullOrWhiteSpace(searchString)) {
                searchString = searchString.ToUpper();
                //objects = objects.Where(o => o.AdresObiektu.First().Miejscowosc.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().KodPocztowy.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().NrDomu.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().NrLokalu.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().Ulica.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().NrDzialki.ToUpper().Contains(searchString) ||
                //o.AdresObiektu.First().Poczta.ToUpper().Contains(searchString) ||
                //o.SposobRozliczenia.ToUpper().Contains(searchString)
                //)
                //.ToList();
                if (currentCategory == "KodPocztowy")
                    objects = objects.Where(o => o.AdresObiektu.First().KodPocztowy.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Miejscowosc")
                    objects = objects.Where(o => o.AdresObiektu.First().Miejscowosc.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "NrDomu")
                    objects = objects.Where(o => o.AdresObiektu.First().NrDomu.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "NrLokalu")
                    objects = objects.Where(o => o.AdresObiektu.First().NrLokalu.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Ulica")
                    objects = objects.Where(o => o.AdresObiektu.First().Ulica.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "NrDzialki")
                    objects = objects.Where(o => o.AdresObiektu.First().NrDzialki.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Poczta")
                    objects = objects.Where(o => o.AdresObiektu.First().Poczta.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "SposobRozliczenia")
                    objects = objects.Where(o => o.SposobRozliczenia.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "IdKlienta")
                    objects = objects.Where(o => o.KlientIdKlienta.ToString().ToUpper().Contains(searchString)).ToList();
                else
                    objects = objects.Where(o => o.Id.ToString().ToUpper().Contains(searchString)).ToList();
            }
            foreach (var objectItem in objects)
            {
                BasicCreateObjectViewModel tempObject = new BasicCreateObjectViewModel
                {
                    NumerKlienta = objectItem.KlientIdKlienta,
                    KodPocztowy = objectItem.AdresObiektu.First().KodPocztowy,
                    Miejscowosc = objectItem.AdresObiektu.First().Miejscowosc,
                    NrDomu = objectItem.AdresObiektu.First().NrDomu,
                    NrLokalu = objectItem.AdresObiektu.First().NrLokalu,
                    NrDzialki = objectItem.AdresObiektu.First().NrDzialki,
                    Poczta = objectItem.AdresObiektu.First().Poczta,
                    SposobRozliczenia = objectItem.SposobRozliczenia.ToString(),
                    Ulica = objectItem.AdresObiektu.First().Ulica,
                    Geometria = objectItem.Geometria.ToString(),
                    Id = objectItem.Id,
                };
                list.Add(tempObject);
            }
            switch (sortOrder)
            {
                case "Id_desc":
                    list = list.OrderByDescending(o => o.Id).ToList();
                    break;
                case "HouseNumber":
                    list = list.OrderBy(o => o.NrDomu).ToList();
                    break;
                case "HouseNumber_desc":
                    list = list.OrderByDescending(o => o.NrDomu).ToList();
                    break;
                case "Street":
                    list = list.OrderBy(o => o.Ulica).ToList();
                    break;
                case "Street_desc":
                    list = list.OrderByDescending(o => o.Ulica).ToList();
                    break;
                case "PostalCode":
                    list = list.OrderBy(o => o.KodPocztowy).ToList();
                    break;
                case "PostalCode_desc":
                    list = list.OrderByDescending(o => o.KodPocztowy).ToList();
                    break;
                case "ApartmentNumber":
                    list = list.OrderBy(o => o.NrLokalu).ToList();
                    break;
                case "ApartmentNumber_desc":
                    list = list.OrderByDescending(o => o.NrLokalu).ToList();
                    break;
                case "PlotNumber":
                    list = list.OrderBy(o => o.NrDzialki).ToList();
                    break;
                case "PlotNumber_desc":
                    list = list.OrderByDescending(o => o.NrDzialki).ToList();
                    break;
                case "Post":
                    list = list.OrderBy(o => o.Poczta).ToList();
                    break;
                case "Post_desc":
                    list = list.OrderByDescending(o => o.Poczta).ToList();
                    break;
                case "Payoff":
                    list = list.OrderBy(o => o.SposobRozliczenia).ToList();
                    break;
                case "Payoff_desc":
                    list = list.OrderByDescending(o => o.SposobRozliczenia).ToList();
                    break;
                case "City":
                    list = list.OrderBy(o => o.Miejscowosc).ToList();
                    break;
                case "City_desc":
                    list = list.OrderByDescending(o => o.Miejscowosc).ToList();
                    break;
                case "ClientId":
                    list = list.OrderBy(o => o.NumerKlienta).ToList();
                    break;
                case "ClientId_desc":
                    list = list.OrderByDescending(o => o.NumerKlienta).ToList();
                    break;
                default:
                    list = list.OrderBy(o => o.Id).ToList();
                    break;
            }
            //List<ClientBasicInformation> list = new List<ClientBasicInformation>();
            //var clients = dbContext.Klient
            //    .Include(k => k.IdentyfikatorPodatkowy)
            //    .ToList();
            //foreach (var client in clients)
            //{
            //    ClientBasicInformation tempClient = new ClientBasicInformation
            //    {
            //        ClientId = client.IdKlienta,
            //        NIP = client.IdentyfikatorPodatkowy.First().Nip,
            //        PESEL = client.IdentyfikatorPodatkowy.First().Pesel,
            //        RodzajKlientaId = (ContractorType)client.RodzajKlientaId
            //    };
            //    list.Add(tempClient);
            //}
            int pageSize = 10;    
            return View(PaginatedList<BasicCreateObjectViewModel>.CreateAsync(list.AsQueryable<BasicCreateObjectViewModel>(), page ?? 1, pageSize));
        }
        public IActionResult Create()
        {
            List<string> payoffTypes = new List<string> { "norma", "wodomierz" };
            ViewBag.payoffTypesList = payoffTypes;
            return PartialView();
        }
        [HttpPost]
        public IActionResult Create([Bind] BasicCreateObjectViewModel objectItem)
        {

            if (ModelState.IsValid)
            {
                if (objectItem.NumerKlienta.HasValue) {
                    int count = dbContext.Klient.Where(k => k.IdKlienta == objectItem.NumerKlienta).ToList().Count;
                    if (count < 1) {
                        ModelState.AddModelError("NumerKlienta", "*Nie ma takiego klienta");
                        return View(objectItem);
                    }      
                }
                
                string[] coordinates = objectItem.Geometria.Split(' ');
            double x = double.Parse(coordinates[0], System.Globalization.CultureInfo.InvariantCulture);
            double y = double.Parse(coordinates[1], System.Globalization.CultureInfo.InvariantCulture);

            var obiekt = new Obiekt()
            {
                Geometria = new Point(x, y),
                SposobRozliczenia = objectItem.SposobRozliczenia,
                KlientIdKlienta = objectItem.NumerKlienta,
                Woda = false,
                Scieki = false,
                AbonamentWoda = false,
                AbonamentScieki = false
            };

            var adresObiektu = new AdresObiektu()
            {
                Miejscowosc = objectItem.Miejscowosc,
                Ulica = objectItem.Ulica,
                NrDomu = objectItem.NrDomu,
                NrLokalu = objectItem.NrLokalu,
                NrDzialki = objectItem.NrDzialki,
                KodPocztowy = objectItem.KodPocztowy,
                Poczta = objectItem.Poczta
            };

            dbContext.Obiekt.Add(obiekt);
            adresObiektu.ObiektId = obiekt.Id;
            dbContext.AdresObiektu.Add(adresObiektu);
            dbContext.SaveChanges();

            return RedirectToAction("AdvancedDetails/" + obiekt.Id, "Object");

            }
            return View(objectItem);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var objects = dbContext
                .Obiekt
                .Join(dbContext.AdresObiektu,
                    o => o.Id,
                    a => a.ObiektId,
                    (o, a) => new { Obiekt = o, AdresObiektu = a })
                .Where(o => o.Obiekt.Id == id)
                .First();
            BasicCreateObjectViewModel objectModel = new BasicCreateObjectViewModel()
            {
                Id = objects.Obiekt.Id,
                NumerKlienta = objects.Obiekt.KlientIdKlienta,
                Geometria = objects.Obiekt.Geometria.ToString(),
                KodPocztowy = objects.AdresObiektu.KodPocztowy,
                Miejscowosc = objects.AdresObiektu.Miejscowosc,
                NrDomu = objects.AdresObiektu.NrDomu,
                NrLokalu = objects.AdresObiektu.NrLokalu,
                NrDzialki = objects.AdresObiektu.NrDzialki,
                Poczta = objects.AdresObiektu.Poczta,
                Ulica = objects.AdresObiektu.Ulica,
                //SposobRozliczeniaId = (TypRozliczenia) objects.Obiekt.SposobRozliczeniaId
            };
            return PartialView(objectModel);
        }

        public IActionResult AdvancedDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var objects = dbContext
                .Obiekt
                .Join(dbContext.AdresObiektu,
                    o => o.Id,
                    a => a.ObiektId,
                    (o, a) => new { Obiekt = o, AdresObiektu = a })
                .Where(o => o.Obiekt.Id == id)
                .First();
            BasicCreateObjectViewModel objectModel = new BasicCreateObjectViewModel()
            {
                NumerKlienta = objects.Obiekt.KlientIdKlienta,
                Id = objects.Obiekt.Id,
                Geometria = objects.Obiekt.Geometria.ToString(),
                KodPocztowy = objects.AdresObiektu.KodPocztowy,
                Miejscowosc = objects.AdresObiektu.Miejscowosc,
                NrDomu = objects.AdresObiektu.NrDomu,
                NrLokalu = objects.AdresObiektu.NrLokalu,
                NrDzialki = objects.AdresObiektu.NrDzialki,
                Poczta = objects.AdresObiektu.Poczta,
                Ulica = objects.AdresObiektu.Ulica,
                //SposobRozliczeniaId = (TypRozliczenia)objects.Obiekt.SposobRozliczeniaId
            };
            return View(objectModel);
        }
       
        public IActionResult GeneralInfo(int id)
        {
            var objects = dbContext
                .Obiekt
                .Join(dbContext.AdresObiektu,
                    o => o.Id,
                    a => a.ObiektId,
                    (o, a) => new { Obiekt = o, AdresObiektu = a })
                .Where(o => o.Obiekt.Id == id)
                .First();
            var geometry = objects.Obiekt.Geometria.ToString();
            BasicCreateObjectViewModel objectModel = new BasicCreateObjectViewModel()
            {
                NumerKlienta = objects.Obiekt.KlientIdKlienta,
                Id = objects.Obiekt.Id,
                Geometria = geometry.Substring(7,geometry.Length-8),
                KodPocztowy = objects.AdresObiektu.KodPocztowy,
                Miejscowosc = objects.AdresObiektu.Miejscowosc,
                NrDomu = objects.AdresObiektu.NrDomu,
                NrLokalu = objects.AdresObiektu.NrLokalu,
                NrDzialki = objects.AdresObiektu.NrDzialki,
                Poczta = objects.AdresObiektu.Poczta,
                Ulica = objects.AdresObiektu.Ulica,
                //SposobRozliczeniaId = (TypRozliczenia)objects.Obiekt.SposobRozliczeniaId
            };
            return PartialView("_Details", objectModel);
        }
        public IActionResult AttachToClient(string objectId)
        {
            var model = new AttachClientToObjectViewModel {Id = Int32.Parse(objectId) };

            return PartialView("_AttachClientModalPartial", model);
        }

        [HttpPost]
        public IActionResult AttachToClient(AttachClientToObjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NumerKlienta.HasValue)
                {
                    int count = dbContext.Klient.Where(k => k.IdKlienta == model.NumerKlienta).ToList().Count;
                    if (count < 1)
                    {
                        ModelState.AddModelError("NumerKlienta", "*Nie ma takiego klienta");
                    }
                    else {
                        var obj = dbContext.Obiekt.Where(o => o.Id == model.Id).ToList().Single();
                        obj.KlientIdKlienta = model.NumerKlienta;
                        dbContext.Obiekt.Update(obj);
                        dbContext.SaveChanges();
                        return Json(new { status = 302, url = "/Object/AdvancedDetails/"+ obj.Id });
                    }
                }     
            }
            return PartialView("_AttachClientModalPartial", model);
        }
        public IActionResult DetachFromClient(int id) {
            var obj = dbContext.Obiekt.Where(o => o.Id == id).ToList().Single();
            obj.KlientIdKlienta = null;
            dbContext.Obiekt.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("AdvancedDetails/" + id, "Object");
        }
        public IActionResult DetachObjectFromClientAndRedirectToClient(int objectId, int clientId)
        {
            var obj = dbContext.Obiekt.Where(o => o.Id == objectId).ToList().Single();
            obj.KlientIdKlienta = null;
            dbContext.Obiekt.Update(obj);
            dbContext.SaveChanges();
            return RedirectToAction("Details/" + clientId, "Client");
        }

        public IActionResult GetObjectAttachedToClient(int clientId)
        {
            List<ObjectInListViewModel> list = new List<ObjectInListViewModel>();
            var objects = dbContext.Obiekt
                .Include(o => o.AdresObiektu)
                .Where(o => o.KlientIdKlienta == clientId)
                .ToList();

            foreach (var objectItem in objects)
            {
                ObjectInListViewModel tempObject = new ObjectInListViewModel
                {
                    IdKlienta = objectItem.KlientIdKlienta,
                    KodPocztowy = objectItem.AdresObiektu.First().KodPocztowy,
                    Miejscowosc = objectItem.AdresObiektu.First().Miejscowosc,
                    NrDomu = objectItem.AdresObiektu.First().NrDomu,
                    NrLokalu = objectItem.AdresObiektu.First().NrLokalu,
                    NrDzialki = objectItem.AdresObiektu.First().NrDzialki,
                    SposobRozliczenia = objectItem.SposobRozliczenia,
                    Ulica = objectItem.AdresObiektu.First().Ulica,
                    Id = objectItem.Id,
                };
                list.Add(tempObject);
            }
            return PartialView("_ClientObjects", list);
        }
        public IActionResult Map(int id)
        {
            var pointsList = dbContext.Obiekt.Where(o => o.Id == id).ToList();
            List<PointFeature> pointList = new List<PointFeature>();
            foreach (Obiekt o in pointsList)
            {
                pointList.Add(new PointFeature(o.Id, o.Geometria));
            }
            return PartialView("~/Views/Object/_Map.cshtml", pointList);
        }
        public IActionResult AttachWaterMeter(string objectId) {
            var model = new AttachWaterMeterToObjectViewModel { IdObiektu = Int32.Parse(objectId) };
            return PartialView("_AttachWaterMeterModalPartial", model);
        }
        [HttpPost]
        public IActionResult AttachWaterMeter(AttachWaterMeterToObjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.NrWodomierza))
                {
                    var waterMeter = dbContext.Wodomierz.Where(w => w.NrWodomierza == model.NrWodomierza).ToList();
                    if (waterMeter.Count >= 1)
                    {
                        var waterMeters = dbContext.Wodomierz.Where(w => w.ObiektId == model.IdObiektu).ToList();
                        if ((waterMeters.Where(w => w.TypWodomierza == "GLOWNY").ToList().Count > 0) && model.TypWodomierza == "GLOWNY") {
                            ModelState.AddModelError("TypWodomierza", "*W tym obiekcie istnieje już wodomierz główny");
                        }
                        else
                        {
                            var item = waterMeter.Single();
                            item.ObiektId = model.IdObiektu;
                            item.TypWodomierza = model.TypWodomierza;
                            dbContext.Wodomierz.Update(item);
                            dbContext.SaveChanges();
                            //return RedirectToAction("AdvancedDetails/" + obj.Id, "Object");
                            return Json(new { status = 302, url = "/Object/AdvancedDetails/" + model.IdObiektu });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("NrWodomierza", "*Nie ma takiego wodomierza");
                    }
                }
            }
            return PartialView("_AttachWaterMeterModalPartial", model);
        }
        public IActionResult DetachWaterMeter(int id)
        {
            var waterMeter = dbContext.Wodomierz.Where(o => o.Id == id).ToList().Single();
            waterMeter.ObiektId = null;
            waterMeter.TypWodomierza = "ODPIETY";
            dbContext.Wodomierz.Update(waterMeter);
            dbContext.SaveChanges();
            return RedirectToAction("AdvancedDetails/" + id, "Object");
        }
        //[HttpPost]
        //public PartialViewResult DisplayEmployees([FromBody] string id)
        //{
        //    int empid = Convert.ToInt32(id);
        //    //According to the id to query the database

        //    List<BasicCreateObjectViewModel> list = new List<BasicCreateObjectViewModel>();
        //    var objects = dbContext.Obiekt
        //        .Include(o => o.AdresObiektu).Where(o => o.Id == empid)
        //        .ToList();

        //    foreach (var objectItem in objects)
        //    {
        //        BasicCreateObjectViewModel tempObject = new BasicCreateObjectViewModel
        //        {
        //            KodPocztowy = objectItem.AdresObiektu.First().KodPocztowy,
        //            Miejscowosc = objectItem.AdresObiektu.First().Miejscowosc,
        //            NrDomu = objectItem.AdresObiektu.First().NrDomu,
        //            NrLokalu = objectItem.AdresObiektu.First().NrLokalu,
        //            NrDzialki = objectItem.AdresObiektu.First().NrDzialki,
        //            Poczta = objectItem.AdresObiektu.First().Poczta,
        //            SposobRozliczenia = objectItem.SposobRozliczenia,
        //            Ulica = objectItem.AdresObiektu.First().Ulica,
        //            Geometria = objectItem.Geometria.ToString(),
        //            Id = objectItem.Id,
        //        };
        //        list.Add(tempObject);
        //    }
        //    return PartialView("Index", list);
        //}
        //public IActionResult Example() {
        //    return View();
        //}
    }
}