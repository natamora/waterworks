using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.PriceList
{
    public class SubscriptionPriceViewModel
    {
        [Display(Name = "Id cennika")]
        public int Id { get; set; }

        [Display(Name = "Okres rozliczeniowy")]
        public string OkresRozliczeniowy { get; set; }

        [Display(Name = "Opłata")]
        public float Oplata { get; set; }

        [Display(Name = "Rodzaj klienta")]
        public string RodzajKlienta { get; set; }
        public int RodzajKlientaId { get; set; }

        [Display(Name = "Nazwa cennika")]
        public string NazwaCennika { get; set; }

        [Display(Name = "Typ cennika")]
        public string TypCennika { get; set; }
    }
}
