using CatalogueNew.Models.Entities;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly ICatalogueContext context;

        public ImageController(ICatalogueContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}