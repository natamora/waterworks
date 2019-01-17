using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.Object
{
    public class ObjectInListViewModel
    {
        [Required]
        [Display(Name = "Id obiektu")]
        public int Id { get; set; }

        public int? IdKlienta { get; set; }

        [Required]
        [Display(Name = "Sposób rozliczenia")]
        public string SposobRozliczenia { get; set; }

        [Required]
        [Display(Name = "Miejscowość")]
        public string Miejscowosc { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        public string KodPocztowy { get; set; }

        [Required]
        [Display(Name = "Nr domu")]
        public string NrDomu { get; set; }

        [Required]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; }

        [Required]
        [Display(Name = "Nr lokalu")]
        public string NrLokalu { get; set; }

        [Display(Name = "Nr działki")]
        public string NrDzialki { get; set; }
    }
}
