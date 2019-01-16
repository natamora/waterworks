using System;
using System.Collections.Generic;

namespace Waterworks.Models.Db.Waterworks
{
    public partial class NormyZuzyciaWody
    {
        public int Id { get; set; }
        public float ZuzycieM3NaMiesiac { get; set; }
        public int NazwaNormy { get; set; }
        public int ZuzycieDm3NaDobe { get; set; }
        public int RodzajKlientaId { get; set; }
        public string OpisNormy { get; set; }
        public string TypNormy { get; set; }

        public RodzajKlienta RodzajKlienta { get; set; }
    }
}
