using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using log4net;
using CatalogueNew.Web.App_Start;

namespace CatalogueNew.Web
{
    public class Global : HttpApplication
    {
        void Application_Error(Object sender, EventArgs e)
        {
            var log = DependencyResolver.Current.GetService<ILog>();
            Exception ex = Server.GetLastError().GetBaseException();

            log.Error("App_Error", ex);
        }

        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
    }
}