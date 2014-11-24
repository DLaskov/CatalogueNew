using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatalogueNew.Models.Services;
using CatalogueNew.Models.Entities;

namespace CatalogueNew.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ProductService ps = new ProductService();
            ViewBag.Product = ps.GetAt(12);
            ViewBag.Product.Category = ps.GetCategory(ViewBag.Product.CategoryID);
            return View();
        }
    }
}