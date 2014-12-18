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
        int Add(Product product);

        void Modify(Product product);

        void Remove(Product product);

        void Remove(int id);

        Product Find(int id);

        PagedList<Product> GetProducts(int page);

        PagedList<Product> GetProductsByManufacturer(int page, int manufacturerID);

        PagedList<Product> GetProducts(int page, int? categoryId, int? manufacturerId);
    }
}
