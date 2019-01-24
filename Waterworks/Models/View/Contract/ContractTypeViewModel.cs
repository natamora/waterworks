using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Waterworks.Models.View.Contract
{
    public class ContractTypeViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Typ")]
        public string Typ { get; set; }
    }
}
