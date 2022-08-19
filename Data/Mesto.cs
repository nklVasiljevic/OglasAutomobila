using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class Mesto
    {
        public Mesto()
        {
            Oglasis = new HashSet<Oglasi>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MestoId { get; set; }
        public string Grad { get; set; }
        public string Opština { get; set; }
        public string Ulica { get; set; }

        public virtual ICollection<Oglasi> Oglasis { get; set; }
    }
}
