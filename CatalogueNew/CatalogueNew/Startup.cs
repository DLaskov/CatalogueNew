using CatalogueNew.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartupAttribute(typeof(CatalogueNew.Web.Startup))]
namespace CatalogueNew.Web
{
    public class Startup
    {
        private static bool IsAjaxRequest(IOwinRequest request)
        {
            IReadableStringCollection query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            IHeaderDictionary headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login"),
                Provider = new CookieAuthenticationProvider
            {
                OnApplyRedirect = ctx =>
                {
                    if (!IsAjaxRequest(ctx.Request))
                    {
                        ctx.Response.Redirect(ctx.RedirectUri);
                    }
                }
            }
            });
        }
    }
}