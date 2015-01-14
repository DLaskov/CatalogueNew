using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogueNew.Models.Services;
using System.Security.Claims;

namespace CatalogueNew.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : AppController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}