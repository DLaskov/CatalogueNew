using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Views
{
    public abstract class AppViewPage<LogInModel> : WebViewPage<LogInModel>
    {
        protected AppUserClaims CurrentUser
        {
            get
            {
                return new AppUserClaims(this.User as ClaimsPrincipal);
            }
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}