using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class ProductService : IProductService
    {
        private ICatalogueContext context;
        
        public ProductService(ICatalogueContext context)
        {
            this.context = context;
        }

        public Infrastructure.PagedList<Product> GetItems(int? page)
        {
            throw new NotImplementedException();
        }

        public Product Find(int? id)
        {
            throw new NotImplementedException();
        }

        public void Add(Product category)
        {
            throw new NotImplementedException();
        }

        public void Modify(Product category)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product category)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
