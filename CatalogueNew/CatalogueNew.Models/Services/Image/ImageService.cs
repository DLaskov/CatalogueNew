using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class ImageService : BaseService, IImageService
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
        public void Remove(int id)
        {
            var image = this.Context.Images.Find(id);
            this.Context.Images.Remove(image);
            this.Context.SaveChanges();
        }
    }
}
