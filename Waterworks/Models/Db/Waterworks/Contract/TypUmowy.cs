using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class TypUmowy
    {
        public TypUmowy()
        {
            Umowa = new HashSet<Umowa>();
        }

        public int Id { get; set; }
        public string Typ { get; set; }

        public ICollection<Umowa> Umowa { get; set; }
    }
}
