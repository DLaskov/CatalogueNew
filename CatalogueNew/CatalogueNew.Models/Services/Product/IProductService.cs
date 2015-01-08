using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IProductService : ICommonService<Product>
    {
        PagedList<Product> GetProducts(int page);

        PagedList<Product> GetProductsByManufacturer(int page, int manufacturerID);

        PagedList<Product> GetProducts(int page, int? categoryId, int? manufacturerId);

        PagedList<Product> GetProducts(int page, string userID);
    }
}
