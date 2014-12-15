using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class ImageService : BaseService<Image>, IImageService
    {
        public ImageService(ICatalogueContext context)
            : base(context)
        {
        }

        public void Add(Image image)
        {
            this.Context.Images.Add(image);
            this.Context.SaveChanges();
        }
    }
}
