using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class WartosciKlasyfikatorow
    {
        public WartosciKlasyfikatorow()
        {
            KlasyfikatoryObiektu = new HashSet<KlasyfikatoryObiektu>();
        }

        public int Id { get; set; }
        public int KlasyfikatoryId { get; set; }

        public Klasyfikatory Klasyfikatory { get; set; }
        public ICollection<KlasyfikatoryObiektu> KlasyfikatoryObiektu { get; set; }
    }
}
