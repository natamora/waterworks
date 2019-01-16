using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class RodzajKlienta
    {
        public RodzajKlienta()
        {
            CennikScieki = new HashSet<CennikScieki>();
            CennikWoda = new HashSet<CennikWoda>();
            Klient = new HashSet<Klient>();
            NormyZuzyciaWody = new HashSet<NormyZuzyciaWody>();
            OplatyAbonamentoweScieki = new HashSet<OplatyAbonamentoweScieki>();
            OplatyAbonamentoweWoda = new HashSet<OplatyAbonamentoweWoda>();
        }

        public int Id { get; set; }
        public string TypKlienta { get; set; }

        public ICollection<CennikScieki> CennikScieki { get; set; }
        public ICollection<CennikWoda> CennikWoda { get; set; }
        public ICollection<Klient> Klient { get; set; }
        public ICollection<NormyZuzyciaWody> NormyZuzyciaWody { get; set; }
        public ICollection<OplatyAbonamentoweScieki> OplatyAbonamentoweScieki { get; set; }
        public ICollection<OplatyAbonamentoweWoda> OplatyAbonamentoweWoda { get; set; }
    }
}
