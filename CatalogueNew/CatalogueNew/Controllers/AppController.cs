using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public abstract class AppController : Controller
    {
        public AppUserClaims CurrentUser
        {
            get
            {
                return new AppUserClaims(this.User as ClaimsPrincipal);
            }
        }
    }
}