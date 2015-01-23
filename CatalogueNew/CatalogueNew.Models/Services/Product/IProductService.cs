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
        int GetProductsPerPage(string id);

        PagedList<Product> GetProducts(int page, int pageSize);

        PagedList<Product> GetProducts(int page, int? categoryId, int? manufacturerId, int pageSize);

        PagedList<Product> GetProducts(int page, string userID, int pageSize);

        PagedList<Product> GetProductsByTag(int page, int tagID, int pageSize);
    }
}
