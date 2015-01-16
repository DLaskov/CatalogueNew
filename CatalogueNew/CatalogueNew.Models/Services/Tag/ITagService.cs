using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface ITagService
    {
        void Add(string tagName, int productID);
        void Remove(int productID, int tagID);
        Tag Find(string tagName);
        List<ProductTag> FindAllTagsForProduct(int productID);
    }
}
