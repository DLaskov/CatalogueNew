using CatalogueNew.Models.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CatalogueNew.Web
{
    public class HttpImageHandler : HttpTaskAsyncHandler
    {
        private ICatalogueContext dbContext = DependencyResolver.Current.GetService<ICatalogueContext>();

        public override bool IsReusable
        {
            get { return false; }
        }
       
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            int ID;
            if (int.TryParse(context.Request.QueryString["id"], out ID))
            {
                var imageMetaData = await dbContext.Images.Where(n => n.ImageID == ID).Select(n => new
                    {
                        ImageID = n.ImageID,
                        LastUpdated = n.LastUpdated,
                        MimeType = n.MimeType
                    }).FirstOrDefaultAsync();

                if (imageMetaData == null)
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                if (!String.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]))
                {
                    DateTimeFormatInfo provider = CultureInfo.InvariantCulture.DateTimeFormat;
                    DateTime lastMod = DateTime.ParseExact(context.Request.Headers["If-Modified-Since"],
                        "r", provider).ToLocalTime();

                    if (imageMetaData.LastUpdated.Equals(lastMod))
                    {
                        context.Response.StatusCode = 304;
                        context.Response.StatusDescription = "Not Modified";
                        return;
                    }
                }

                byte[] imageData = await dbContext.Images.Where(x => x.ImageID == ID)
                            .Select(x => x.Value).SingleAsync();
                context.Response.ContentType = imageMetaData.MimeType;
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.Cache.SetLastModified(imageMetaData.LastUpdated);
                await context.Response.OutputStream.WriteAsync(imageData, 0, imageData.Length);
            }
            else
            {
                context.Response.StatusCode = 404;
            }

        }

    }
}