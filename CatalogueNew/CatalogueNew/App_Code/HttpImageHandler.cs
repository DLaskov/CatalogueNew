using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageHandler
{
    public class HttpImageHandler : IHttpHandler
    {
        ICatalogueContext dbContext;
        public HttpImageHandler(ICatalogueContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int ID;
            if (int.TryParse(context.Request.QueryString["id"], out ID))
            {
                Image image = dbContext.Images.Single(x => x.ImageID == ID);
                if (!String.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    var lastMod = DateTime.ParseExact(context.Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();
                    if (lastMod == image.UpdatedAt)
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        return;
                    }
                    byte[] imageData = image.Value;
                    context.Response.OutputStream.Write(imageData, 0, imageData.Length);
                    context.Response.Cache.SetCacheability(HttpCacheability.Public);
                    context.Response.Cache.SetLastModified(image.UpdatedAt);
                }

            }
            else
            {
                context.Response.StatusCode = 404;
            }

        }
    }
}