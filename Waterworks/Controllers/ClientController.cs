using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Waterworks.Models.View.Client;
using Waterworks.Models;
using Microsoft.EntityFrameworkCore;
using Waterworks.Data;
using Waterworks.Models.Db.Waterworks;

namespace Waterworks.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ClientController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index(string sortOrder, string currentCategory, string category, string currentFilter, string searchString, int? page)
        {
            ViewData["IdSortParam"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["CompanyNameSortParam"] = sortOrder == "CompanyName" ? "CompanyName_desc" : "CompanyName";
            ViewData["ClientTypeSortParam"] = sortOrder == "ClientType" ? "ClientType_desc" : "ClientType";
            ViewData["NameSortParam"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SurnameSortParam"] = sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewData["NIPSortParam"] = sortOrder == "NIP" ? "NIP_desc" : "NIP";
            ViewData["PESELSortParam"] = sortOrder == "PESEL" ? "PESEL_desc" : "PESEL";
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

            List<ClientBasicInformation> list = new List<ClientBasicInformation>();
            var clients = dbContext.Klient
                .Include(k => k.IdentyfikatorPodatkowy)
                .Include(k => k.DaneKlienta)
                .ToList();

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToUpper();
                if (currentCategory == "RodzajKlienta")
                    clients = clients.Where(o => o.RodzajKlientaId.ToString().ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "NazwaFirmy")
                    clients = clients.Where(o => o.DaneKlienta.First().NazwaFirmy.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Imie")
                    clients = clients.Where(o => o.DaneKlienta.First().Imie.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "Nazwisko")
                    clients = clients.Where(o => o.DaneKlienta.First().Nazwisko.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "NIP")
                    clients = clients.Where(o => o.IdentyfikatorPodatkowy.First().Nip.ToUpper().Contains(searchString)).ToList();
                else if (currentCategory == "PESEL")
                    clients = clients.Where(o => o.IdentyfikatorPodatkowy.First().Pesel.ToUpper().Contains(searchString)).ToList();
                else
                    clients = clients.Where(o => o.IdKlienta.ToString().ToUpper().Contains(searchString)).ToList();
            }
            foreach (var client in clients)
            {
                ClientBasicInformation tempClient = new ClientBasicInformation
                {
                    NazwaFirmy = client.DaneKlienta.First().NazwaFirmy,
                    ClientId = client.IdKlienta,
                    Imie = client.DaneKlienta.First().Imie,
                    Nazwisko = client.DaneKlienta.First().Nazwisko,
                    NIP = client.IdentyfikatorPodatkowy.First().Nip,
                    PESEL = client.IdentyfikatorPodatkowy.First().Pesel,
                    RodzajKlientaId = (ContractorType)client.RodzajKlientaId
                };
                list.Add(tempClient);
            }
            switch (sortOrder) {
                case "Id_desc":
                    list = list.OrderByDescending(c => c.ClientId).ToList();
                    break;
                case "CompanyName":
                    list = list.OrderBy(c => c.NazwaFirmy).ToList();
                    break;
                case "CompanyName_desc":
                    list = list.OrderByDescending(c => c.NazwaFirmy).ToList();
                    break;
                case "ClientType":
                    list = list.OrderBy(c => c.RodzajKlientaId).ToList();
                    break;
                case "ClientType_desc":
                    list = list.OrderByDescending(c => c.RodzajKlientaId).ToList();
                    break;
                case "Name":
                    list = list.OrderBy(c => c.Imie).ToList();
                    break;
                case "Name_desc":
                    list = list.OrderByDescending(c => c.Imie).ToList();
                    break;
                case "Surname":
                    list = list.OrderBy(c => c.Nazwisko).ToList();
                    break;
                case "Surname_desc":
                    list = list.OrderByDescending(c => c.Nazwisko).ToList();
                    break;
                case "NIP":
                    list = list.OrderBy(c => c.NIP).ToList();
                    break;
                case "NIP_desc":
                    list = list.OrderByDescending(c => c.NIP).ToList();
                    break;
                case "PESEL":
                    list = list.OrderBy(c => c.PESEL).ToList();
                    break;
                case "PESEL_desc":
                    list = list.OrderByDescending(c => c.PESEL).ToList();
                    break;
                default:
                    list = list.OrderBy(c => c.ClientId).ToList();
                    break;
            }
            int pageSize = 10;
            return View(PaginatedList<ClientBasicInformation>.CreateAsync(list.AsQueryable<ClientBasicInformation>(), page ?? 1, pageSize));

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind] ClientCreateViewModel client)
        {
            if (ModelState.IsValid)
            {
                var identyfikatorPodatkowy = new IdentyfikatorPodatkowy()
                {
                    Pesel = client.PESEL,
                    Nip = client.NIP,
                    Regon = client.REGON,
                    Krs = client.KRS
                };

                var adresKorespondencyjny = new AdresKorespondencyjny()
                {
                    KodPocztowy = client.AdresKorespondencyjny.KodPocztowy,
                    Miejscowosc = client.AdresKorespondencyjny.Miejscowosc,
                    Poczta = client.AdresKorespondencyjny.Poczta,
                    Ulica = client.AdresKorespondencyjny.Ulica,
                    NrDomu = client.AdresKorespondencyjny.NrDomu,
                    NrLokalu = client.AdresKorespondencyjny.NrLokalu
                };

                var daneKlienta = new DaneKlienta()
                {
                    Imie = client.Imie,
                    Nazwisko = client.Nazwisko,
                    Miejscowosc = client.Miejscowosc,
                    KodPocztowy = client.KodPocztowy,
                    Poczta = client.Poczta,
                    Ulica = client.Ulica,
                    NrDomu = client.NrDomu,
                    NrLokalu = client.NrLokalu,
                    Telefon = client.Telefon,
                    Email = client.Email,
                    AdresKorespondencyjny = adresKorespondencyjny
                };
                var aktywnyOdbiorca = new AktywnyOdbiorca()
                {
                    Imie = client.AktywnyOdbiorca.Imie,
                    Nazwisko = client.AktywnyOdbiorca.Nazwisko,
                    Miejscowosc = client.AktywnyOdbiorca.Miejscowosc,
                    KodPocztowy = client.AktywnyOdbiorca.KodPocztowy,
                    Poczta = client.AktywnyOdbiorca.Poczta,
                    Ulica = client.AktywnyOdbiorca.Ulica,
                    NrDomu = client.AktywnyOdbiorca.NrDomu,
                    NrLokalu = client.AktywnyOdbiorca.NrLokalu,
                    Telefon = client.AktywnyOdbiorca.Telefon,
                    Email = client.AktywnyOdbiorca.Email
                };

                var klient = new Klient()
                {
                    CzyAktywny = true,
                    RodzajKlientaId = (int)client.RodzajKlientaId
                };

                dbContext.Klient.Add(klient);
                dbContext.AdresKorespondencyjny.Add(adresKorespondencyjny);

                daneKlienta.AdresKorespondencyjnyId = adresKorespondencyjny.Id;
                daneKlienta.IdKlienta = klient.IdKlienta;
                dbContext.DaneKlienta.Add(daneKlienta);

                aktywnyOdbiorca.KlientIdKlienta = klient.IdKlienta;
                dbContext.AktywnyOdbiorca.Add(aktywnyOdbiorca);

                identyfikatorPodatkowy.KlientIdKlienta = klient.IdKlienta;
                dbContext.IdentyfikatorPodatkowy.Add(identyfikatorPodatkowy);

                dbContext.SaveChanges();

                return RedirectToAction("Details/" + klient.IdKlienta, "Client");
            }
            return View(client);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = dbContext
                .Klient
                .Where(k => k.IdKlienta == id)
                .Include(d => d.DaneKlienta)
                .Include(d => d.AktywnyOdbiorca)
                .Include(d => d.IdentyfikatorPodatkowy)
                .Include(d => d.RodzajKlienta)
                .First();

            var correspondenceAdress = client.DaneKlienta.Join(dbContext.AdresKorespondencyjny,
                o => o.AdresKorespondencyjnyId, a => a.Id,
                (o, a) => new { DaneKlienta = o, AdresKorespondencyjny = a })
                .Where(o => o.DaneKlienta.AdresKorespondencyjnyId == client.DaneKlienta.First().AdresKorespondencyjnyId)
                .First();

            ActiveReceiverViewModel activeReceiver = new ActiveReceiverViewModel()
            {
                Id = client.AktywnyOdbiorca.First().Id,
                Imie = client.AktywnyOdbiorca.First().Imie,
                Nazwisko = client.AktywnyOdbiorca.First().Nazwisko,
                Miejscowosc = client.AktywnyOdbiorca.First().Miejscowosc,
                KodPocztowy = client.AktywnyOdbiorca.First().KodPocztowy,
                Poczta = client.AktywnyOdbiorca.First().Poczta,
                Ulica = client.AktywnyOdbiorca.First().Ulica,
                NrDomu = client.AktywnyOdbiorca.First().NrDomu,
                NrLokalu = client.AktywnyOdbiorca.First().NrLokalu,
                Telefon = client.AktywnyOdbiorca.First().Telefon,
                Telefon2 = client.AktywnyOdbiorca.First().Telefon2,
                Email = client.AktywnyOdbiorca.First().Email,
                Email2 = client.AktywnyOdbiorca.First().Email2,
                KlientIdKlienta = client.AktywnyOdbiorca.First().KlientIdKlienta
            };
            CorrespondenceAdressViewModel correspondenceAddress = new CorrespondenceAdressViewModel()
            {
                Id = correspondenceAdress.AdresKorespondencyjny.Id,
                KodPocztowy = correspondenceAdress.AdresKorespondencyjny.KodPocztowy,
                Miejscowosc = correspondenceAdress.AdresKorespondencyjny.Miejscowosc,
                NrDomu = correspondenceAdress.AdresKorespondencyjny.NrDomu,
                NrLokalu = correspondenceAdress.AdresKorespondencyjny.NrLokalu,
                Poczta = correspondenceAdress.AdresKorespondencyjny.Poczta,
                Ulica = correspondenceAdress.AdresKorespondencyjny.Ulica
            };
            ClientCreateViewModel clientModel = new ClientCreateViewModel()
            {
                Id = client.IdKlienta,
                Imie = client.DaneKlienta.First().Imie,
                Nazwisko = client.DaneKlienta.First().Nazwisko,
                Telefon = client.DaneKlienta.First().Telefon,
                RodzajKlientaId = (ContractorType)client.RodzajKlienta.Id,
                Email = client.DaneKlienta.First().Email,
                NIP = client.IdentyfikatorPodatkowy.First().Nip,
                PESEL = client.IdentyfikatorPodatkowy.First().Pesel,
                REGON = client.IdentyfikatorPodatkowy.First().Regon,
                KRS = client.IdentyfikatorPodatkowy.First().Krs,
                Miejscowosc = client.DaneKlienta.First().Miejscowosc,
                KodPocztowy = client.DaneKlienta.First().KodPocztowy,
                Poczta = client.DaneKlienta.First().Poczta,
                Ulica = client.DaneKlienta.First().Ulica,
                NrDomu = client.DaneKlienta.First().NrDomu,
                NrLokalu = client.DaneKlienta.First().NrLokalu,
                DodatkowyEmail = client.DaneKlienta.First().Email2,
                DodatkowyTelefon = client.DaneKlienta.First().Telefon2,
                AdresKorespondencyjny = correspondenceAddress,
                AktywnyOdbiorca = activeReceiver
            };
            return View(clientModel);
        }

    }
}