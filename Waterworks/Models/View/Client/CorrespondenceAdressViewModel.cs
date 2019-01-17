using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class CorrespondenceAdressViewModel
    {
        //adres korespondencyjny
        public int Id { get; set; }

        [Display(Name = "Miejscowość")]
        public string Miejscowosc { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string KodPocztowy { get; set; }

        [Display(Name = "Poczta")]
        public string Poczta { get; set; }

        [Display(Name = "Ulica")]
        public string Ulica { get; set; }

        [Display(Name = "Nr domu")]
        public string NrDomu { get; set; }

        [Display(Name = "Nr lokalu")]
        public string NrLokalu { get; set; }
    }
}
