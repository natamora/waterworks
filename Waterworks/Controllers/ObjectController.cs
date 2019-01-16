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
        public IActionResult Index()
        {
            List<BasicCreateObjectViewModel> list = new List<BasicCreateObjectViewModel>();
            var objects = dbContext.Obiekt
                .Include(o => o.AdresObiektu)
                .ToList();

            foreach (var objectItem in objects)
            {
                BasicCreateObjectViewModel tempObject = new BasicCreateObjectViewModel
                {
                    KodPocztowy = objectItem.AdresObiektu.First().KodPocztowy,
                    Miejscowosc = objectItem.AdresObiektu.First().Miejscowosc,
                    NrDomu = objectItem.AdresObiektu.First().NrDomu,
                    NrLokalu = objectItem.AdresObiektu.First().NrLokalu,
                    NrDzialki = objectItem.AdresObiektu.First().NrDzialki,
                    Poczta = objectItem.AdresObiektu.First().Poczta,
                    SposobRozliczenia = objectItem.SposobRozliczenia,
                    Ulica = objectItem.AdresObiektu.First().Ulica,
                    Geometria = objectItem.Geometria.ToString(),
                    Id = objectItem.Id,
                };
                list.Add(tempObject);
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
            return View(list);
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
                KlientIdKlienta = objectItem.NumerKlienta
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

        public IActionResult AttachToClient(string id)
        {
            var model = new AttachClientToObjectViewModel {Id = Int32.Parse(id) };

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
                        //return RedirectToAction("AdvancedDetails/" + obj.Id, "Object");
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