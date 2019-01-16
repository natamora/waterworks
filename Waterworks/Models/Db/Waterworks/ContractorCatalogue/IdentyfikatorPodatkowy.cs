using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class IdentyfikatorPodatkowy
    {
        public int Id { get; set; }
        public string Pesel { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Krs { get; set; }
        public int KlientIdKlienta { get; set; }

        public Klient KlientIdKlientaNavigation { get; set; }
    }
}
