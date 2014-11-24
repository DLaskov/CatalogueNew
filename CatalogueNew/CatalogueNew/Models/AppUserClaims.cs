using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class AppUserClaims : ClaimsPrincipal
    {
        public AppUserClaims(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }
    }
}