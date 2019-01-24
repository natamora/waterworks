using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.WaterMeter
{
    public class WaterMeterViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nr wodomierza")]
        public string NrWodomierza { get; set; }
        [Display(Name = "Data legalizacji")]
        public DateTime DataLegalizacji { get; set; }
        [Display(Name = "Data ewidencji")]
        public DateTime DataEwidencji { get; set; }
        [Display(Name = "Id obiektu")]
        public int? ObiektId { get; set; }
        [Display(Name = "Typ wodomierza")]
        public string TypWodomierza{get; set;}
    }
}
