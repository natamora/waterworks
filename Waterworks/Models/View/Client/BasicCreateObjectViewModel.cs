using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class BasicCreateObjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sposób rozliczenia")]
        public string SposobRozliczenia { get; set; }

        [RegularExpression("[0-9]+")]
        [Display(Name = "Numer klienta")]
        public int? NumerKlienta { get; set; }

        [Required]
        [Display(Name = "Wspołrzędne")]
        public string Geometria { get; set; }

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

        [Required]
        [Display(Name = "Poczta")]
        public string Poczta { get; set; }
    }
}
