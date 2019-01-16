using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Waterworks.Models.View.Client
{
    public class AttachClientToObjectViewModel
    {
        public int Id { get; set; }

        [RegularExpression("[0-9]+")]
        [Display(Name = "Numer klienta")]
        public int? NumerKlienta { get; set; }
    }
}
