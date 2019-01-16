using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class Klient
    {
        public Klient()
        {
            AktywnyOdbiorca = new HashSet<AktywnyOdbiorca>();
            DaneKlienta = new HashSet<DaneKlienta>();
            IdentyfikatorPodatkowy = new HashSet<IdentyfikatorPodatkowy>();
            Obiekt = new HashSet<Obiekt>();
        }

        public int IdKlienta { get; set; }
        public bool CzyAktywny { get; set; }
        public int RodzajKlientaId { get; set; }

        public RodzajKlienta RodzajKlienta { get; set; }
        public ICollection<AktywnyOdbiorca> AktywnyOdbiorca { get; set; }
        public ICollection<DaneKlienta> DaneKlienta { get; set; }
        public ICollection<IdentyfikatorPodatkowy> IdentyfikatorPodatkowy { get; set; }
        public ICollection<Obiekt> Obiekt { get; set; }
    }
}
