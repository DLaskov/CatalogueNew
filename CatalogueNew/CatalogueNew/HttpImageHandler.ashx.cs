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
                var query = dbContext.Images.Where(n => n.ImageID == ID).Select(n => new
                {
                    ImageID = n.ImageID,
                    LastUpdated = n.LastUpdated,
                    MimeType = n.MimeType
                }).FirstOrDefault();

                if (query == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                if (!String.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
                {
                    DateTimeFormatInfo provider = CultureInfo.InvariantCulture.DateTimeFormat;
                    DateTime lastMod = DateTime.ParseExact(context.Request.Headers["If-Modified-Since"],
                        "r", provider).ToLocalTime();

                    if (query.LastUpdated.Equals(lastMod))
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        return;
                    }                    
                }

                byte[] imageData = dbContext.Images.Where(x => x.ImageID == ID).Single().Value;
                context.Response.ContentType = query.MimeType;
                context.Response.OutputStream.Write(imageData, 0, imageData.Length);
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetLastModified(query.LastUpdated);
            }
            else
            {
                context.Response.StatusCode = 404;
            }

        }
    }
}