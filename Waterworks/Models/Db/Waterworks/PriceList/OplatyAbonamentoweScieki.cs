using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class OplatyAbonamentoweScieki
    {
        public int Id { get; set; }
        public string OkresRozliczeniowy { get; set; }
        public float Oplata { get; set; }
        public int RodzajKlientaId { get; set; }
        public string Nazwa { get; set; }
        public string TypCennika { get; set; }

        public RodzajKlienta RodzajKlienta { get; set; }
    }
}
