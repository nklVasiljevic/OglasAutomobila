using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace OglasAutomobila.Data
{
    public partial class AspNetUserClaim : IdentityUserClaim<int>
    {
        public override int Id { get; set; }
        public override int UserId { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
