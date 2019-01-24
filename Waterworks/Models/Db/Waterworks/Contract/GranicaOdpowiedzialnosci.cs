using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class GranicaOdpowiedzialnosci
    {
        public GranicaOdpowiedzialnosci()
        {
            UmowaGranicaOdpowiedzialnosciScieki = new HashSet<Umowa>();
            UmowaGranicaOdpowiedzialnosciWoda = new HashSet<Umowa>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Typ { get; set; }

        public ICollection<Umowa> UmowaGranicaOdpowiedzialnosciScieki { get; set; }
        public ICollection<Umowa> UmowaGranicaOdpowiedzialnosciWoda { get; set; }
    }
}
