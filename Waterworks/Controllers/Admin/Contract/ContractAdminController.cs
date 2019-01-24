using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;
using Waterworks.Models.View.Contract;

namespace Waterworks.Controllers.Admin.Contract
{
    [Authorize(Roles = "Admin")]
    public class ContractAdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ContractAdminController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult ContractTypeList()
        {
            List<ContractTypeViewModel> list = new List<ContractTypeViewModel>();
            var priceLists = dbContext.TypUmowy.ToList();                

            foreach (var item in priceLists)
            {
                ContractTypeViewModel tempObject = new ContractTypeViewModel
                {
                    Id = item.Id,
                    Typ = item.Typ
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/Contract/Type/_List.cshtml", list);
        }
        [HttpGet]
        public IActionResult ContractTypeCreate()
        {
            return PartialView("~/Views/Contract/Type/_Create.cshtml");
        }

        [HttpPost]
        public IActionResult ContractTypeCreate([Bind] ContractTypeViewModel priceItem)
        {
            var item = new TypUmowy()
            {
                Typ = priceItem.Typ
            };
            dbContext.TypUmowy.Add(item);
            dbContext.SaveChanges();

            return Json(new { status = 302, url = "/Admin/Index"});
        }
        public IActionResult ContractTypeDelete(int id) {
            var contractType = dbContext.TypUmowy.Where(t => t.Id == id).ToList().First();
            dbContext.TypUmowy.Remove(contractType);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public IActionResult LiabilityLimitList()
        {
            List<LiabilityLimitViewModel> list = new List<LiabilityLimitViewModel>();
            var liabilityLimits = dbContext.GranicaOdpowiedzialnosci.ToList();

            foreach (var item in liabilityLimits)
            {
                LiabilityLimitViewModel tempObject = new LiabilityLimitViewModel
                {
                    Id = item.Id,
                    Nazwa = item.Nazwa,
                    Typ = item.Typ
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/Contract/LiabilityLimit/_List.cshtml", list);
        }
        [HttpGet]
        public IActionResult LiabilityLimitCreate()
        {
            List<string> types = new List<string> { "WODA", "SCIEKI" };
            ViewBag.types = types;
            return PartialView("~/Views/Contract/LiabilityLimit/_Create.cshtml");
        }

        [HttpPost]
        public IActionResult LiabilityLimitCreate([Bind] LiabilityLimitViewModel priceItem)
        {
            var item = new GranicaOdpowiedzialnosci()
            {
                Typ = priceItem.Typ,
                Nazwa = priceItem.Nazwa
            };
            dbContext.GranicaOdpowiedzialnosci.Add(item);
            dbContext.SaveChanges();

            return Json(new { status = 302, url = "/Admin/Index" });
        }
        public IActionResult LiabilityLimitDelete(int id)
        {
            var liabilityLimit = dbContext.GranicaOdpowiedzialnosci.Where(t => t.Id == id).ToList().First();
            dbContext.GranicaOdpowiedzialnosci.Remove(liabilityLimit);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult LegalTitleList()
        {
            List<LegalTitleViewModel> list = new List<LegalTitleViewModel>();
            var legalTitles = dbContext.TytulPrawny.ToList();

            foreach (var item in legalTitles)
            {
                LegalTitleViewModel tempObject = new LegalTitleViewModel
                {
                    Id = item.Id,
                    Nazwa = item.Nazwa
                };
                list.Add(tempObject);
            }
            return PartialView("~/Views/Contract/LegalTitle/_List.cshtml", list);
        }
        [HttpGet]
        public IActionResult LegalTitleCreate()
        {
            return PartialView("~/Views/Contract/LegalTitle/_Create.cshtml");
        }

        [HttpPost]
        public IActionResult LegalTitleCreate([Bind] LegalTitleViewModel priceItem)
        {
            var item = new TytulPrawny()
            {
                Nazwa = priceItem.Nazwa
            };
            dbContext.TytulPrawny.Add(item);
            dbContext.SaveChanges();

            return Json(new { status = 302, url = "/Admin/Index" });
        }
        public IActionResult LegalTitleDelete(int id)
        {
            var legalTitle = dbContext.TytulPrawny.Where(t => t.Id == id).ToList().First();
            dbContext.TytulPrawny.Remove(legalTitle);
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
    }
}