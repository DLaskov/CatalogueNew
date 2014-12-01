using CatalogueNew.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class ManufacturerController : Controller
    {
        private IManufacturerService manufacturerService;

        // GET: Manufacturer
        public ActionResult Index()
        {
            return View();
        }

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
        }


    }
}