using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class Klasyfikatory
    {
        public Klasyfikatory()
        {
            KlasyfikatoryObiektu = new HashSet<KlasyfikatoryObiektu>();
            WartosciKlasyfikatorow = new HashSet<WartosciKlasyfikatorow>();
        }

        public int Id { get; set; }
        public string NazwaKlasyfikatora { get; set; }

        public ICollection<KlasyfikatoryObiektu> KlasyfikatoryObiektu { get; set; }
        public ICollection<WartosciKlasyfikatorow> WartosciKlasyfikatorow { get; set; }
    }
}
