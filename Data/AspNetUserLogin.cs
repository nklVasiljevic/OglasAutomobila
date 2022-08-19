using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class AspNetUserLogin : IdentityUserLogin<int>
    {
        public  int LoginProvider { get; set; }
        public  int ProviderKey { get; set; }
        public override string ProviderDisplayName { get; set; }
        public override int UserId { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
