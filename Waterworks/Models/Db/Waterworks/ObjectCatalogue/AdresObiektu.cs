using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class AdresObiektu
    {
        public int Id { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        public string NrDzialki { get; set; }
        public string KodPocztowy { get; set; }
        public string Poczta { get; set; }
        public int ObiektId { get; set; }

        public Obiekt Obiekt { get; set; }
    }
}
