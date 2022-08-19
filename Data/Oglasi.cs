using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class Oglasi
    {
        public Oglasi()
        {
            OglasiUsers = new HashSet<OglasiUser>();
        }

        public int OglasiId { get; set; }
        public string Naziv { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public int? MestoId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        public DateTime? Godište { get; set; }
        public bool Registrovan { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy.}")]
        public DateTime? RegistrovanDo { get; set; }
        public string Opis { get; set; }

        public virtual Mesto Mesto { get; set; }
        public virtual ICollection<OglasiUser> OglasiUsers { get; set; }
    }
}
