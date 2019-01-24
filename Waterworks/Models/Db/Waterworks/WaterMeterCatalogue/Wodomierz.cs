using System;
using System.Collections.Generic;
using Waterworks.Models.Db.Waterworks;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class Wodomierz
    {
        public Wodomierz()
        {
            FormulyZmniejszajace = new HashSet<FormulyZmniejszajace>();
        }

        public int Id { get; set; }
        public string NrWodomierza { get; set; }
        public DateTime DataEwidencji { get; set; }
        public DateTime DataLegalizacji { get; set; }
        public int? ObiektId { get; set; }
        public string TypWodomierza { get; set; }

        public Obiekt Obiekt { get; set; }
        public ICollection<FormulyZmniejszajace> FormulyZmniejszajace { get; set; }
    }
}
