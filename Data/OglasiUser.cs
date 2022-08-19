using System;
using System.Collections.Generic;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class OglasiUser
    {
        public int OglasiUser1 { get; set; }
        public int? OglasiId { get; set; }
        public int? UserId { get; set; }

        public virtual Oglasi Oglasi { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
