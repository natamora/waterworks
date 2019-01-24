using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.WaterMeter
{
    public class ChangeWaterMeterViewModel
    {
        [Display(Name = "Nr wodomierza")]
        public string NrWodomierza { get; set; }

        [Display(Name = "Typ wodomierza")]
        public string TypWodomierza { get; set; }

        [Display(Name = "Nr nowego wodomierza")]
        public string NrNowegoWodomierza { get; set; }

        [RegularExpression("[0-9]+")]
        [Display(Name = "Id obiektu")]
        public int? IdObiektu { get; set; }
    }
}
