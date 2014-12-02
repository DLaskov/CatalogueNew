using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IProductService
    {
        PagedList<Product> GetItems(int page);

        Product Find(int id);

        void Add(Product category);

        void Modify(Product category);

        void Remove(Product category);

        void Remove(int id);
    }
}
