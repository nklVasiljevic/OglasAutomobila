using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using OglasAutomobila.Data;

namespace OglasAutomobila.Models
{
    public class OglasModel
    {
        public virtual DbSet<Oglasi> Oglasis { get; set; }

    }
}
