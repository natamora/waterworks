using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class CennikScieki
    {
        public int Id { get; set; }
        public float CenaZaM3 { get; set; }
        public int RodzajKlientaId { get; set; }
        public string NazwaCennika { get; set; }
        public string TypCennika { get; set; }

        public RodzajKlienta RodzajKlienta { get; set; }
    }
}
