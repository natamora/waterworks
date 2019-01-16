using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class ActiveReceiverViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

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

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Dodatkowy telefon")]
        public string Telefon2 { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Dodatkowy email")]
        public string Email2 { get; set; }

        public int KlientIdKlienta { get; set; }
    }
}
