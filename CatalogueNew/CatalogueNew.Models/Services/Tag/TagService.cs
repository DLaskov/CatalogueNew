using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CatalogueNew.Models.Services
{
    public class TagService : BaseService, ITagService
    {
        public TagService(ICatalogueContext context) :
            base(context)
        {

        }

        public void Add(string tagName, int productID)
        {
            tagName = tagName.ToLower();
            Tag currentTag = Find(tagName);
            if (currentTag == null)
            {
                Context.Tags.Add(new Tag() { Name = tagName });
                Context.SaveChanges();
                currentTag = Find(tagName);
            }
            ProductTag currentProductTag = new ProductTag() { TagID = currentTag.TagID, ProductID = productID };
            Context.ProductsTags.Add(currentProductTag);
            Context.SaveChanges();
        }

        public void Remove(int tagID, int productID)
        {
            ProductTag tagToRemove = Context.ProductsTags.Where(c => c.ProductID == productID && c.TagID == tagID).SingleOrDefault();
            Context.ProductsTags.Remove(tagToRemove);
            Context.SaveChanges();
        }

        public Tag Find(string tagName)
        {
            return Context.Tags.Where(c => c.Name == tagName).SingleOrDefault();      
        }

        public List<ProductTag> FindAllTagsForProduct(int productID)
        {
            return Context.ProductsTags.Where(c => c.ProductID == productID).Include(c => c.Tag).ToList();
        }
        //public List<Product> GetProductsByTag(int tagID)
        //{
        //    return 
        //}
    }
}
