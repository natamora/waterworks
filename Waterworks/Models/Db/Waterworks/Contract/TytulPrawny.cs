using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class TytulPrawny
    {
        public TytulPrawny()
        {
            Umowa = new HashSet<Umowa>();
        }

        public int Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Umowa> Umowa { get; set; }
    }
}
