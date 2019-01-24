using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class FormulyZmniejszajace
    {
        public int Id { get; set; }
        public int ObiektId { get; set; }
        public int WodomierzId { get; set; }

        public Obiekt Obiekt { get; set; }
        public Wodomierz Wodomierz { get; set; }
    }
}
