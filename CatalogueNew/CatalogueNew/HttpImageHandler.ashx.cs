using CatalogueNew.Models.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web
{
    public class HttpImageHandler : IHttpHandler
    {
        private ICatalogueContext dbContext = DependencyResolver.Current.GetService<ICatalogueContext>();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            int ID;
            if (int.TryParse(context.Request.QueryString["id"], out ID))
            {
                try
                {
                    Image image = dbContext.Images.Single(x => x.ImageID == ID);
                    if (!String.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
                    {
                        DateTimeFormatInfo provider = CultureInfo.InvariantCulture.DateTimeFormat;
                        DateTime lastMod = DateTime.ParseExact(context.Request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();
                        if (image.LastUpdated.Equals(lastMod))
                        {
                            context.Response.StatusCode = 304;
                            context.Response.StatusDescription = "Not Modified";
                            return;
                        }
                        getImage(context, image);
                    }
                    getImage(context, image);
                }
                catch
                {
                    context.Response.StatusCode = 404;
                }
            }
            else
            {
                context.Response.StatusCode = 404;
            }

        }
        private void getImage(HttpContext context, Image image)
        {
            byte[] imageData = image.Value;
            context.Response.ContentType = image.MimeType;
            context.Response.OutputStream.Write(imageData, 0, imageData.Length);
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetLastModified(image.LastUpdated);
        }
    }
}