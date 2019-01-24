using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.Contract
{
    public class BasicContractViewModel
    {
        [Display(Name = "Nr umowy")]
        public string NrUmowy { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data zawarcia")]
        public DateTime DataZawarcia { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data obowiązywania od")]
        public DateTime DataObowiazywaniaOd { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data obowiązywania do")]
        public DateTime DataObowiazywaniaDo { get; set; }
        [Display(Name = "Typ umowy")]
        public string TypUmowy { get; set; }
        [Display(Name = "Uwagi")]
        public string Uwagi { get; set; }
        [Display(Name = "Id obiektu")]
        public int IdObiektu { get; set; }
    }
}
