using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;
using Waterworks.Models.View.Contract;

namespace Waterworks.Controllers
{
    public class ContractController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ContractController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index(string sortOrder, string currentCategory, string category, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ContractNumberSortParam"] = String.IsNullOrEmpty(sortOrder) ? "ContractNumber_desc" : "";
            ViewData["AgreementDateSortParam"] = sortOrder == "AgreementDate" ? "AgreementDate_desc" : "AgreementDate";
            ViewData["FromDateSortParam"] = sortOrder == "FromDate" ? "FromDate_desc" : "FromDate";
            ViewData["ToDateSortParam"] = sortOrder == "ToDate" ? "ToDate_desc" : "ToDate";
            ViewData["ContractTypeSortParam"] = sortOrder == "ContractType" ? "ContractType_desc" : "ContractType";
            ViewData["CommentSortParam"] = sortOrder == "Comment" ? "Comment_desc" : "Comment";
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
            var contracts = dbContext.Umowa
                .Include(o => o.TypUmowy)
                .ToList();

            List<BasicContractViewModel> list = new List<BasicContractViewModel>();
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToUpper();
                if (currentCategory == "DataZawarcia")
                    contracts = contracts.Where(o => o.DataZawarcia.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "DataObowiazywaniaOd")
                    contracts = contracts.Where(o => o.DataObowiazywaniaOd.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "DataObowiazywaniaDo")
                    contracts = contracts.Where(o => o.DataObowiazywaniaDo.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "TypUmowy")
                    contracts = contracts.Where(o => o.TypUmowy.Typ.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Uwagi")
                    contracts = contracts.Where(o => o.Uwagi.ToUpper().Contains(searchString)).ToList();
                else
                    contracts = contracts.Where(o => o.NrUmowy.ToUpper().Contains(searchString)).ToList();
            }
            foreach (var contract in contracts) {
                var item = new BasicContractViewModel()
                {
                    NrUmowy = contract.NrUmowy,
                    TypUmowy = contract.TypUmowy.Typ,
                    DataZawarcia = contract.DataZawarcia,
                    DataObowiazywaniaOd = contract.DataObowiazywaniaOd,
                    DataObowiazywaniaDo = contract.DataObowiazywaniaDo,
                    Uwagi = contract.Uwagi,
                    IdObiektu = contract.ObiektId
                };
                list.Add(item);
            }
            switch (sortOrder)
            {
                case "ContractNumber_desc":
                    list = list.OrderByDescending(o => o.NrUmowy).ToList();
                    break;
                case "AgreementDate":
                    list = list.OrderBy(o => o.DataZawarcia).ToList();
                    break;
                case "AgreementDate_desc":
                    list = list.OrderByDescending(o => o.DataZawarcia).ToList();
                    break;
                case "FromDate":
                    list = list.OrderBy(o => o.DataObowiazywaniaOd).ToList();
                    break;
                case "FromDate_desc":
                    list = list.OrderByDescending(o => o.DataObowiazywaniaOd).ToList();
                    break;
                case "ToDate":
                    list = list.OrderBy(o => o.DataObowiazywaniaDo).ToList();
                    break;
                case "ToDate_desc":
                    list = list.OrderByDescending(o => o.DataObowiazywaniaDo).ToList();
                    break;
                case "ContractType":
                    list = list.OrderBy(o => o.TypUmowy).ToList();
                    break;
                case "ContractType_desc":
                    list = list.OrderByDescending(o => o.TypUmowy).ToList();
                    break;
                case "CommentSort":
                    list = list.OrderBy(o => o.Uwagi).ToList();
                    break;
                case "CommentSort_desc":
                    list = list.OrderByDescending(o => o.Uwagi).ToList();
                    break;
                default:
                    list = list.OrderBy(o => o.NrUmowy).ToList();
                    break;
            }
            int pageSize = 10;
            return View(PaginatedList<BasicContractViewModel>.CreateAsync(list.AsQueryable<BasicContractViewModel>(), page ?? 1, pageSize));

        }

        public IActionResult ObjectContractList(int id)
        {
            var contracts = dbContext.Umowa
                .Include(o => o.TypUmowy)
                .Where(o => o.KlientIdKlienta == id)
                .ToList();

            List<BasicContractViewModel> list = new List<BasicContractViewModel>();
            foreach (var contract in contracts)
            {
                var item = new BasicContractViewModel()
                {
                    NrUmowy = contract.NrUmowy,
                    TypUmowy = contract.TypUmowy.Typ,
                    DataZawarcia = contract.DataZawarcia,
                    DataObowiazywaniaOd = contract.DataObowiazywaniaOd,
                    DataObowiazywaniaDo = contract.DataObowiazywaniaDo,
                    Uwagi = contract.Uwagi,
                    IdObiektu = contract.ObiektId
                };
                list.Add(item);
            }
            return PartialView("~/Views/Contract/_List.cshtml", list);
        }
        public IActionResult Create(int id)
        {
            var contractTypes = dbContext.TypUmowy.ToList();
            List<ContractTypeViewModel> types = new List<ContractTypeViewModel>();
            foreach (var type in contractTypes)
            {
                var item = new ContractTypeViewModel
                {
                    Id = type.Id,
                    Typ = type.Typ
                };
                types.Add(item);
            }
            var legalTitles = dbContext.TytulPrawny.ToList();
            List<LegalTitleViewModel> titles = new List<LegalTitleViewModel>();
            foreach (var title in legalTitles)
            {
                var item = new LegalTitleViewModel
                {
                    Id = title.Id,
                    Nazwa = title.Nazwa
                };
                titles.Add(item);
            }
            var liabilityLimitsWater = dbContext.GranicaOdpowiedzialnosci.Where(g => g.Typ == "WODA").ToList();
            var limitsWater = new List<LiabilityLimitViewModel>();
            foreach (var limit in liabilityLimitsWater)
            {
                var item = new LiabilityLimitViewModel
                {
                    Id = limit.Id,
                    Nazwa = limit.Nazwa,
                    
                };
                limitsWater.Add(item);
            }

            var liabilityLimitsSewage = dbContext.GranicaOdpowiedzialnosci.Where(g => g.Typ == "SCIEKI").ToList();
            var limitsSewage = new List<LiabilityLimitViewModel>();
            foreach (var limit in liabilityLimitsSewage)
            {
                var item = new LiabilityLimitViewModel
                {
                    Id = limit.Id,
                    Nazwa = limit.Nazwa,
                };
                limitsSewage.Add(item);
            }

            var objects = dbContext.Obiekt.Where(o => o.KlientIdKlienta == id).ToList();
            var objs = new List<int>();
            foreach (var o in objects)
            { 
                objs.Add(o.Id);
            }

            var contract = new CreateContractViewModel
            {
                IdKlienta = id,
                TypyUmow = types,
                TytulyPrawne = titles,
                GraniceOdpowiedzialnosciWoda = limitsWater,
                GraniceOdpowiedzialnosciScieki = limitsSewage,
                DataZawarcia = DateTime.Now,
                DataObowiazywaniaOd = DateTime.Now,
                DataObowiazywaniaDo = DateTime.Now,
                Obiekty = objs,
                NrUmowy = Guid.NewGuid().ToString()
            };
            
            return View(contract);
        }

        [HttpPost]
        public IActionResult Create(CreateContractViewModel model)
        {
            var contract = new Umowa()
            {
                NrUmowy = model.NrUmowy,
                DataZawarcia = model.DataZawarcia,
                DataObowiazywaniaOd = model.DataObowiazywaniaOd,
                DataObowiazywaniaDo = model.DataObowiazywaniaDo,
                DeklarowanaIloscM3 = model.DeklarowanaIloscM3,
                GranicaOdpowiedzialnosciWodaId = model.GranicaOdpowiedzialnosciWodaId,
                GranicaOdpowiedzialnosciSciekiId = model.GranicaOdpowiedzialnosciSciekiId,
                KlientIdKlienta = model.IdKlienta,
                ObiektId = model.IdObiektu,
                TypUmowyId = model.TypUmowyId,
                TytulPrawnyId = model.TytulPrawnyId,
                Uwagi = model.Uwagi
            };
            dbContext.Umowa.Add(contract);
            dbContext.SaveChanges();
            return RedirectToAction("Details/" + model.IdKlienta, "Client");
        }
        public IActionResult Details(string id) {
            var contract = dbContext.Umowa
                .Where(u => u.NrUmowy == id)
                .Include(o => o.TytulPrawny)
                .Include(o => o.TypUmowy)
                .Include(o => o.GranicaOdpowiedzialnosciWoda)
                .Include(o => o.GranicaOdpowiedzialnosciScieki)
                .First();

            var contractModel = new ContractViewModel()
            {
                NrUmowy = contract.NrUmowy,
                DataObowiazywaniaOd = contract.DataObowiazywaniaOd,
                DataObowiazywaniaDo = contract.DataObowiazywaniaDo,
                DataZawarcia = contract.DataZawarcia,
                DeklarowanaIloscM3 = contract.DeklarowanaIloscM3,
                IdObiektu = contract.ObiektId,
                IdKlienta = contract.KlientIdKlienta,
                Uwagi = contract.Uwagi,
                GranicaOdpowiedzialnosciWoda = contract.GranicaOdpowiedzialnosciWoda.Nazwa,
                GranicaOdpowiedzialnosciScieki = contract.GranicaOdpowiedzialnosciScieki.Nazwa,
                TypUmowy = contract.TypUmowy.Typ,
                TytulPrawny = contract.TytulPrawny.Nazwa 
            };
            return View(contractModel);
        }

    }
}