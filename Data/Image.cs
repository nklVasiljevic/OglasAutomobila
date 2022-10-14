using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace OglasAutomobila.Data
{
    [Serializable]
    public class Image
    {
        public Image()
        {
            Oglasis = new HashSet<Oglasi>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get;set ;}

        public virtual ICollection<Oglasi> Oglasis { get; set; }
    }
}
