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
        public IActionResult Index()
        {
            List<ClientBasicInformation> list = new List<ClientBasicInformation>();
            var clients = dbContext.Klient
                .Include(k => k.IdentyfikatorPodatkowy)
                .ToList();
            foreach (var client in clients)
            {
                ClientBasicInformation tempClient = new ClientBasicInformation
                {
                    ClientId = client.IdKlienta,
                    NIP = client.IdentyfikatorPodatkowy.First().Nip,
                    PESEL = client.IdentyfikatorPodatkowy.First().Pesel,
                    RodzajKlientaId = (ContractorType)client.RodzajKlientaId
                };
                list.Add(tempClient);
            }
            return View(list);
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
                    KodPocztowy = client.CorrespondeceAddress.KodPocztowy,
                    Miejscowosc = client.CorrespondeceAddress.Miejscowosc,
                    Poczta = client.CorrespondeceAddress.Poczta,
                    Ulica = client.CorrespondeceAddress.Ulica,
                    NrDomu = client.CorrespondeceAddress.NrDomu,
                    NrLokalu = client.CorrespondeceAddress.NrLokalu
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
                    Imie = client.ActiveReceiver.Imie,
                    Nazwisko = client.ActiveReceiver.Nazwisko,
                    Miejscowosc = client.ActiveReceiver.Miejscowosc,
                    KodPocztowy = client.ActiveReceiver.KodPocztowy,
                    Poczta = client.ActiveReceiver.Poczta,
                    Ulica = client.ActiveReceiver.Ulica,
                    NrDomu = client.ActiveReceiver.NrDomu,
                    NrLokalu = client.ActiveReceiver.NrLokalu,
                    Telefon = client.ActiveReceiver.Telefon,
                    Email = client.ActiveReceiver.Email
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
            ClientCreateViewModel clientModel = new ClientCreateViewModel()
            {
                Id = client.IdKlienta,
                Imie = client.DaneKlienta.First().Imie,
                Nazwisko = client.DaneKlienta.First().Nazwisko,
                Telefon = client.DaneKlienta.First().Telefon,
                RodzajKlientaId = (ContractorType)client.RodzajKlienta.Id,
                Email = client.DaneKlienta.First().Email
            };
            return View(clientModel);
        }

    }
}