using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;
using Waterworks.Models.View.PriceList;

namespace Waterworks.Controllers.Admin.Prices.Subscription
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public SubscriptionController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult WaterPriceList()
        {
            List<SubscriptionPriceViewModel> list = new List<SubscriptionPriceViewModel>();
            var priceLists = dbContext.OplatyAbonamentoweWoda
                .Join(dbContext.RodzajKlienta,
                    o => o.RodzajKlientaId,
                    a => a.Id,
                    (o, a) => new { OplatyAbonamentoweWoda = o, RodzajKlienta = a }
                    ).ToList();

            foreach (var item in priceLists)
            {
                SubscriptionPriceViewModel tempObject = new SubscriptionPriceViewModel
                {
                    Id = item.OplatyAbonamentoweWoda.Id,
                    OkresRozliczeniowy = item.OplatyAbonamentoweWoda.OkresRozliczeniowy,
                    Oplata = item.OplatyAbonamentoweWoda.Oplata,
                    NazwaCennika = item.OplatyAbonamentoweWoda.Nazwa,
                    TypCennika = item.OplatyAbonamentoweWoda.TypCennika,
                    RodzajKlienta = item.RodzajKlienta.TypKlienta,
                    RodzajKlientaId = item.RodzajKlienta.Id
                };
                list.Add(tempObject);
            }
            return View("~/Views/PriceList/WaterSubscription/List.cshtml", list);
        }
        [HttpGet]
        public IActionResult WaterPriceCreate()
        {
            List<string> clientTypes = new List<string> { "osoba fizyczna", "podmiot gospodarczy" };
            ViewBag.clientTypesList = clientTypes;
            return View("~/Views/PriceList/WaterSubscription/Create.cshtml");
        }

        [HttpPost]
        public IActionResult WaterPriceCreate([Bind] SubscriptionPriceViewModel priceItem)
        {

            var item = new OplatyAbonamentoweWoda()
            {
                OkresRozliczeniowy = priceItem.OkresRozliczeniowy,
                Oplata = priceItem.Oplata,
                RodzajKlientaId = priceItem.RodzajKlientaId,
                Nazwa = priceItem.NazwaCennika,
                TypCennika = priceItem.TypCennika
            };
            dbContext.OplatyAbonamentoweWoda.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("SewagePriceList", "PriceList");
        }

        public IActionResult SewagePriceList()
        {
            List<SubscriptionPriceViewModel> list = new List<SubscriptionPriceViewModel>();
            var priceLists = dbContext.OplatyAbonamentoweScieki
                .Join(dbContext.RodzajKlienta,
                    o => o.RodzajKlientaId,
                    a => a.Id,
                    (o, a) => new { OplatyAbonamentoweScieki = o, RodzajKlienta = a }
                    ).ToList();

            foreach (var item in priceLists)
            {
                SubscriptionPriceViewModel tempObject = new SubscriptionPriceViewModel
                {
                    Id = item.OplatyAbonamentoweScieki.Id,
                    OkresRozliczeniowy = item.OplatyAbonamentoweScieki.OkresRozliczeniowy,
                    Oplata = item.OplatyAbonamentoweScieki.Oplata,
                    NazwaCennika = item.OplatyAbonamentoweScieki.Nazwa,
                    TypCennika = item.OplatyAbonamentoweScieki.TypCennika,
                    RodzajKlienta = item.RodzajKlienta.TypKlienta,
                    RodzajKlientaId = item.RodzajKlienta.Id
                };
                list.Add(tempObject);
            }
            return View("~/Views/PriceList/SewageSubscription/List.cshtml", list);
        }
        [HttpGet]
        public IActionResult SewagePriceCreate()
        {
            List<string> clientTypes = new List<string> { "osoba fizyczna", "podmiot gospodarczy" };
            ViewBag.clientTypesList = clientTypes;
            return View("~/Views/PriceList/SewageSubscription/Create.cshtml");
        }

        [HttpPost]
        public IActionResult SewagePriceCreate([Bind] SubscriptionPriceViewModel priceItem)
        {

            var item = new OplatyAbonamentoweScieki()
            {
                OkresRozliczeniowy = priceItem.OkresRozliczeniowy,
                Oplata = priceItem.Oplata,
                RodzajKlientaId = priceItem.RodzajKlientaId,
                Nazwa = priceItem.NazwaCennika,
                TypCennika = priceItem.TypCennika
            };
            dbContext.OplatyAbonamentoweScieki.Add(item);
            dbContext.SaveChanges();

            return RedirectToAction("SewagePriceList", "Subscritpion");
        }
    }
}