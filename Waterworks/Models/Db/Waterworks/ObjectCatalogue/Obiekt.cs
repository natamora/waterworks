using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class Obiekt
    {
        public Obiekt()
        {
            AdresObiektu = new HashSet<AdresObiektu>();
        }

        public int Id { get; set; }
        public int? KlientIdKlienta { get; set; }
        public Point Geometria { get; set; }
        public string SposobRozliczenia { get; set; }
        public int? AktywnyOdbiorcaId { get; set; }

        public AktywnyOdbiorca AktywnyOdbiorca { get; set; }
        public Klient KlientIdKlientaNavigation { get; set; }
        public KlasyfikatoryObiektu KlasyfikatoryObiektu { get; set; }
        public ICollection<AdresObiektu> AdresObiektu { get; set; }
    }
}
