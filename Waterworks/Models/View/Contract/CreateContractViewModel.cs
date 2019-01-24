using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.Contract
{
    public class CreateContractViewModel
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
        [RegularExpression("[0-9]+,[0-9]*")]
        [Display(Name = "Deklarowana ilość m3")]
        public float DeklarowanaIloscM3 { get; set; }
        [Display(Name = "Uwagi")]
        public string Uwagi { get; set; }
        public string LokalizacjaUmowy { get; set; }

        [Display(Name = "Typ umowy")]
        public int TypUmowyId { get; set; }
        public List<ContractTypeViewModel> TypyUmow { get; set; }
        [Display(Name = "Tytuł prawny")]
        public int TytulPrawnyId { get; set; }
        public List<LegalTitleViewModel> TytulyPrawne { get; set; }
        [Display(Name = "Granica odpowiedzialności - woda")]
        public int GranicaOdpowiedzialnosciWodaId { get; set; }
        public List<LiabilityLimitViewModel> GraniceOdpowiedzialnosciWoda { get; set; }
        [Display(Name = "Granica odpowiedzialności - ścieki")]
        public int GranicaOdpowiedzialnosciSciekiId { get; set; }
        public List<LiabilityLimitViewModel> GraniceOdpowiedzialnosciScieki { get; set; }
        public int IdKlienta { get; set; }
        [Display(Name = "Id obiektu")]
        public int IdObiektu { get; set; }
        public List<int> Obiekty { get; set; }
    }
}
