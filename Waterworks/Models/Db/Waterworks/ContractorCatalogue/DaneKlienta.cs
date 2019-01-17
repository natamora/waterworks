using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class DaneKlienta
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NazwaFirmy { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Poczta { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public int? AdresKorespondencyjnyId { get; set; }
        public int IdKlienta { get; set; }

        public AdresKorespondencyjny AdresKorespondencyjny { get; set; }
        public Klient IdKlientaNavigation { get; set; }
    }
}
