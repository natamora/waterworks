using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;
using Waterworks.Models.View.PriceList;

namespace Waterworks.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class PriceListController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PriceListController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult WaterPriceList()
        {
            List<PriceViewModel> list = new List<PriceViewModel>();
            var priceLists = dbContext.CennikWoda
                .Join(dbContext.RodzajKlienta,
                    o => o.RodzajKlientaId,
                    a => a.Id,
                    (o, a) => new { CennikWoda = o, RodzajKlienta = a }
                    ).ToList();

            foreach (var item in priceLists)
            {
                PriceViewModel tempObject = new PriceViewModel
                {
                    Id = item.CennikWoda.Id,
                    CenaZaM3 = item.CennikWoda.CenaZaM3,
                    NazwaCennika = item.CennikWoda.NazwaCennika,
                    TypCennika = item.CennikWoda.TypCennika,
                    RodzajKlienta = item.RodzajKlienta.TypKlienta,
                    RodzajKlientaId = item.RodzajKlienta.Id
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/PriceList/Water/_List.cshtml", list);
        }

        [HttpGet]
        public IActionResult WaterPriceCreate()
        {
            List<string> clientTypes = new List<string> { "osoba fizyczna", "podmiot gospodarczy" };
            ViewBag.clientTypesList = clientTypes;
            return View("~/Views/PriceList/Water/Create.cshtml");
        }

        [HttpPost]
        public IActionResult WaterPriceCreate([Bind] PriceViewModel priceItem)
        {

            var item = new CennikWoda()
            {
                NazwaCennika = priceItem.NazwaCennika,
                CenaZaM3 = priceItem.CenaZaM3,
                RodzajKlientaId = priceItem.RodzajKlientaId,
                TypCennika = priceItem.TypCennika
            };
            dbContext.CennikWoda.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("Admin", "Index");
        }

        public IActionResult SewagePriceList()
        {
            List<PriceViewModel> list = new List<PriceViewModel>();
            var priceLists = dbContext.CennikScieki 
                .Join(dbContext.RodzajKlienta,
                    o => o.RodzajKlientaId,
                    a => a.Id,
                    (o, a) => new { CennikScieki = o, RodzajKlienta = a }
                    ).ToList();

            foreach (var item in priceLists)
            {
                PriceViewModel tempObject = new PriceViewModel
                {
                    Id = item.CennikScieki.Id,
                    CenaZaM3 = item.CennikScieki.CenaZaM3,
                    NazwaCennika = item.CennikScieki.NazwaCennika,
                    TypCennika = item.CennikScieki.TypCennika,
                    RodzajKlienta = item.RodzajKlienta.TypKlienta,
                    RodzajKlientaId = item.RodzajKlienta.Id
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/PriceList/Sewage/_List.cshtml", list);
        }

        [HttpGet]
        public IActionResult SewagePriceCreate()
        {
            List<string> clientTypes = new List<string> { "osoba fizyczna", "podmiot gospodarczy" };
            ViewBag.clientTypesList = clientTypes;
            return View("~/Views/PriceList/Sewage/Create.cshtml");
        }

        [HttpPost]
        public IActionResult SewagePriceCreate([Bind] PriceViewModel priceItem)
        {

            var item = new CennikScieki()
            {
                NazwaCennika = priceItem.NazwaCennika,
                CenaZaM3 = priceItem.CenaZaM3,
                RodzajKlientaId = priceItem.RodzajKlientaId,
                TypCennika = priceItem.TypCennika
            };
            dbContext.CennikScieki.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("Admin", "Index");
        }
    }
}