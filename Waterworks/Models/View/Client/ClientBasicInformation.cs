using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class ClientBasicInformation
    {
        [Display(Name = "Nr klienta")]
        public int ClientId { get; set; }

        [EnumDataType(typeof(ContractorType))]
        [Display(Name = "Rodzaj klienta")]
        public ContractorType RodzajKlientaId { get; set; }


        [Display(Name = "Imię")]
        public string Imie { get; set; }

        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string NazwaFirmy { get; set; }

        public string PESEL { get; set; }
        public string NIP { get; set; }

    }
}
