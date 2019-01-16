using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.PriceList
{
    public class PriceViewModel
    {
        [Display(Name = "Id cennika")]
        public int Id { get; set; }
        [Display(Name = "Cena za m3")]
        public float CenaZaM3 { get; set; }
        [Display(Name = "Rodzaj klienta")]
        public string RodzajKlienta { get; set; }

        public int RodzajKlientaId { get; set; }
        [Display(Name = "Nazwa cennika")]
        public string NazwaCennika { get; set; }
        [Display(Name = "Typ cennika")]
        public string TypCennika { get; set; }
    }
}
