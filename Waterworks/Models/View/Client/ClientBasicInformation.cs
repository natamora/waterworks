using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class ClientBasicInformation
    {
        [Display(Name = "Numer klienta")]
        public int ClientId { get; set; }

        [EnumDataType(typeof(ContractorType))]
        [Display(Name = "Rodzaj klienta")]
        public ContractorType RodzajKlientaId { get; set; }

        public string PESEL { get; set; }
        public string NIP { get; set; }

    }
}
