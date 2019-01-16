using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class AktywnyOdbiorca
    {
        public AktywnyOdbiorca()
        {
            Obiekt = new HashSet<Obiekt>();
        }

        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
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
        public int KlientIdKlienta { get; set; }

        public Klient KlientIdKlientaNavigation { get; set; }
        public ICollection<Obiekt> Obiekt { get; set; }
    }
}
