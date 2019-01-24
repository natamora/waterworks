using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Data;
using Waterworks.Models.View.WaterMeter;

namespace Waterworks.Controllers
{   
    public class WaterMeterController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public WaterMeterController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index(string sortOrder, string currentCategory, string category, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["WaterMeterNumberSortParam"] = String.IsNullOrEmpty(sortOrder) ? "WaterMeterNumber_desc" : "";
            ViewData["ObjectIdSortParam"] = sortOrder == "ObjectId" ? "ObjectId_desc" : "ObjectId";
            ViewData["LegalizationDateSortParam"] = sortOrder == "LegalizationDate" ? "LegalizationDate_desc" : "LegalizationDate";
            ViewData["RecordDateSortParam"] = sortOrder == "RecordDate" ? "RecordDate_desc" : "RecordDate";
            ViewData["WaterMeterTypeSortParam"] = sortOrder == "WaterMeterType" ? "WaterMeterType_desc" : "WaterMeterType";
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
            List<WaterMeterViewModel> list = new List<WaterMeterViewModel>();
            var waterMeters = dbContext.Wodomierz.ToList();
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToUpper();
                if (currentCategory == "IdObiektu")
                    waterMeters = waterMeters.Where(o => o.ObiektId.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "DataLegalizacji")
                    waterMeters = waterMeters.Where(o => o.DataLegalizacji.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "DataEwidencji")
                    waterMeters = waterMeters.Where(o => o.DataEwidencji.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "TypWodomierza")
                    waterMeters = waterMeters.Where(o => o.TypWodomierza.ToUpper().Contains(searchString)).ToList();
                else
                    waterMeters = waterMeters.Where(o => o.NrWodomierza.ToUpper().Contains(searchString)).ToList();
            }
            foreach (var objectItem in waterMeters)
            {
                WaterMeterViewModel tempObject = new WaterMeterViewModel
                {
                    Id = objectItem.Id,
                    NrWodomierza = objectItem.NrWodomierza,
                    DataEwidencji = objectItem.DataEwidencji,
                    DataLegalizacji = objectItem.DataLegalizacji,
                    ObiektId = objectItem.ObiektId,
                    TypWodomierza = objectItem.TypWodomierza
                };
                list.Add(tempObject);
            }
            switch (sortOrder)
            {
                case "WaterMeterNumber_desc":
                    list = list.OrderByDescending(o => o.NrWodomierza).ToList();
                    break;
                case "ObjectId":
                    list = list.OrderBy(o => o.ObiektId).ToList();
                    break;
                case "ObjectId_desc":
                    list = list.OrderByDescending(o => o.ObiektId).ToList();
                    break;
                case "LegalizationDate":
                    list = list.OrderBy(o => o.DataLegalizacji).ToList();
                    break;
                case "LegalizationDate_desc":
                    list = list.OrderByDescending(o => o.DataLegalizacji).ToList();
                    break;
                case "RecordDate":
                    list = list.OrderBy(o => o.DataEwidencji).ToList();
                    break;
                case "RecordDate_desc":
                    list = list.OrderByDescending(o => o.DataEwidencji).ToList();
                    break;
                case "WaterMeterType":
                    list = list.OrderBy(o => o.TypWodomierza).ToList();
                    break;
                case "WaterMeterType_desc":
                    list = list.OrderByDescending(o => o.TypWodomierza).ToList();
                    break;
                default:
                    list = list.OrderBy(o => o.NrWodomierza).ToList();
                    break;
            }
            int pageSize = 10;
            return View(PaginatedList<WaterMeterViewModel>.CreateAsync(list.AsQueryable<WaterMeterViewModel>(), page ?? 1, pageSize));
        }
        public IActionResult List(int id) {
            List<WaterMeterViewModel> list = new List<WaterMeterViewModel>();
            var waterMeters = dbContext.Wodomierz.Where(w => w.ObiektId == id).ToList();
            foreach (var objectItem in waterMeters)
            {
                WaterMeterViewModel tempObject = new WaterMeterViewModel
                {
                    Id = objectItem.Id,
                    NrWodomierza = objectItem.NrWodomierza,
                    DataEwidencji = objectItem.DataEwidencji,
                    DataLegalizacji = objectItem.DataLegalizacji,
                    ObiektId = objectItem.ObiektId,
                    TypWodomierza = objectItem.TypWodomierza
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/WaterMeter/_List.cshtml", list);
        }
        public IActionResult DetachWaterMeter(int id)
        {
            var waterMeter = dbContext.Wodomierz.Where(w => w.Id == id).First();
            var objectId = waterMeter.ObiektId;

            return RedirectToAction("AdvancedDetails/" + objectId, "Object");
        }

        public IActionResult ChangeWaterMeter(int objectId, string oldWaterMeterNumber, string oldWaterMeterType) {
            
            var model = new ChangeWaterMeterViewModel{ IdObiektu = objectId, NrWodomierza = oldWaterMeterNumber, TypWodomierza = oldWaterMeterType};
            return PartialView("_ChangeWaterMeterModalPartial", model);
        }
        [HttpPost]
        public IActionResult ChangeWaterMeter(ChangeWaterMeterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.NrNowegoWodomierza))
                {
                    var newWaterMeter = dbContext.Wodomierz.Where(w => w.NrWodomierza == model.NrNowegoWodomierza).ToList();
                    if (newWaterMeter.Count >= 1)
                    {
                        var item = newWaterMeter.Single();
                        if (!item.ObiektId.HasValue)
                        {
                            item.ObiektId = model.IdObiektu;
                            item.TypWodomierza = model.TypWodomierza;
                            dbContext.Wodomierz.Update(item);

                            var oldWaterMeter = dbContext.Wodomierz.Where(w => w.NrWodomierza == model.NrWodomierza).First();
                            oldWaterMeter.ObiektId = null;
                            oldWaterMeter.TypWodomierza = "WOLNY";
                            dbContext.Wodomierz.Update(oldWaterMeter);

                            dbContext.SaveChanges();
                            return Json(new { status = 302, url = "/Object/AdvancedDetails/" + model.IdObiektu });
                        }
                        else {
                            ModelState.AddModelError("NrNowegoWodomierza", "*Ten wodomierz jest już podpięty pod inny obiekt: " + item.ObiektId);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("NrNowegoWodomierza", "*Nie ma takiego wodomierza");
                    }
                }
            }
            return PartialView("_ChangeWaterMeterModalPartial", model);
        }
        //public IActionResult AttachToObject(string id)
        //{
        //    var model = new AttachWaterMeterToObjectViewModel { Id = Int32.Parse(id) };

        //    return PartialView("_AttachClientModalPartial", model);
        //}
        //[HttpPost]
        //public IActionResult AttachToClient(AttachClientToObjectViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (model.NumerKlienta.HasValue)
        //        {
        //            int count = dbContext.Klient.Where(k => k.IdKlienta == model.NumerKlienta).ToList().Count;
        //            if (count < 1)
        //            {
        //                ModelState.AddModelError("NumerKlienta", "*Nie ma takiego klienta");
        //            }
        //            else
        //            {
        //                var obj = dbContext.Obiekt.Where(o => o.Id == model.Id).ToList().Single();
        //                obj.KlientIdKlienta = model.NumerKlienta;
        //                dbContext.Obiekt.Update(obj);
        //                dbContext.SaveChanges();
        //                //return RedirectToAction("AdvancedDetails/" + obj.Id, "Object");
        //                return Json(new { status = 302, url = "/Object/AdvancedDetails/" + obj.Id });
        //            }
        //        }
        //    }
        //    return PartialView("_AttachClientModalPartial", model);
        //}
    }
}