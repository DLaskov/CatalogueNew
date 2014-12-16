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

        public void SaveUploadedFile(ProductViewModel model, HttpPostedFileBase file)
        {
            var path = Path.Combine(Server.MapPath("~/Content/TempImages/"), Path.GetTempFileName());
            file.SaveAs(path);
            if (model.ImagePaths == null)
            {
                model.ImagePaths = new List<string>();
            }
            model.ImagePaths.Add(path);
            UpdateModel<ProductViewModel>(model);
            //        using (BinaryReader reader = new BinaryReader(file.InputStream))
            //        {
            //            binaryData = reader.ReadBytes((int)file.InputStream.Length);
            //        }

            //        Image image = new Image
            //        {
            //            MimeType = file.ContentType,
            //            LastUpdated = DateTime.Now,
            //            Value = binaryData
            //        };

            //        context.Images.Add(image);
            //        context.SaveChanges();
            //    }
            //}

            //if (isSavedSuccessfully)
            //{
            //    return Json(new { Message = fName });
            //}
            //else
            //{
            //    return Json(new { Message = "Error in saving file" });
            //}
        }
    }
}