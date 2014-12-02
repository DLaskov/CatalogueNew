using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
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

        //[HttpPost]
        //public ActionResult Upload()
        //{
        //    Image image = new Image
        //    {
        //        ImageName = "Test image",
        //        MimeType = "image/jpeg",
        //        UpdatedAt = DateTime.Now,

        //    };
        //}
    }
}