using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class AdresKorespondencyjny
    {
        public AdresKorespondencyjny()
        {
            DaneKlienta = new HashSet<DaneKlienta>();
        }

        public int Id { get; set; }
        public string Miejscowosc { get; set; }
        public string KodPocztowy { get; set; }
        public string Poczta { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }

        public ICollection<DaneKlienta> DaneKlienta { get; set; }
    }
}
