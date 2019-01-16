using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class KlasyfikatoryObiektu
    {
        public int ObiektId { get; set; }
        public int KlasyfikatoryId { get; set; }
        public int WartosciKlasyfikatorowId { get; set; }

        public Klasyfikatory Klasyfikatory { get; set; }
        public Obiekt Obiekt { get; set; }
        public WartosciKlasyfikatorow WartosciKlasyfikatorow { get; set; }
    }
}
