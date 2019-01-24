using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class Umowa
    {
        public string NrUmowy { get; set; }
        public DateTime DataZawarcia { get; set; }
        public DateTime DataObowiazywaniaOd { get; set; }
        public DateTime DataObowiazywaniaDo { get; set; }
        public int KlientIdKlienta { get; set; }
        public int ObiektId { get; set; }
        public int TytulPrawnyId { get; set; }
        public float DeklarowanaIloscM3 { get; set; }
        public string Uwagi { get; set; }
        public string LokalizacjaUmowy { get; set; }
        public int GranicaOdpowiedzialnosciWodaId { get; set; }
        public int GranicaOdpowiedzialnosciSciekiId { get; set; }
        public int TypUmowyId { get; set; }

        public GranicaOdpowiedzialnosci GranicaOdpowiedzialnosciScieki { get; set; }
        public GranicaOdpowiedzialnosci GranicaOdpowiedzialnosciWoda { get; set; }
        public Klient KlientIdKlientaNavigation { get; set; }
        public Obiekt Obiekt { get; set; }
        public TypUmowy TypUmowy { get; set; }
        public TytulPrawny TytulPrawny { get; set; }
    }
}
