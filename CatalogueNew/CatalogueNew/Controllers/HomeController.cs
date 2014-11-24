using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogueNew.Models.Services;

namespace CatalogueNew.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ProductService ps = new ProductService();
            ViewBag.Product = ps.GetAt(0);
            return View();
        }
    }
}