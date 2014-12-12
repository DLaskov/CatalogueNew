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

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    byte[] binaryData;

                    using (BinaryReader reader = new BinaryReader(file.InputStream))
                    {
                        binaryData = reader.ReadBytes((int)file.InputStream.Length);
                    }

                    Image image = new Image
                    {
                        MimeType = file.ContentType,
                        LastUpdated = DateTime.Now,
                        Value = binaryData
                    };

                    context.Images.Add(image);
                    context.SaveChanges();
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}