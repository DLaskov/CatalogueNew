using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class ProductService : BaseService, IProductService
    {
        private const int pageSize = 3;

        public ProductService(ICatalogueContext context)
            : base(context)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            return this.Context.Products.ToList();
        }

        public Product Find(int? id)
        {
            return this.Context.Products.Find(id);
        }

        public void Add(Product product)
        {
            this.Context.Products.Add(product);
            this.Context.SaveChanges();
        }

        public void Modify(Product product)
        {
            this.Context.Entry(product).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Remove(Product product)
        {
            this.Context.Products.Remove(product);
            this.Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var product = this.Find(id);
            this.Context.Products.Remove(product);
            this.Context.SaveChanges();
        }

        public PagedList<Product> GetItems(int? page)
        {
            var pagedList = new PagedList<Product>(this.Context.Products.OrderBy(c => c.Name), page, pageSize);
            return pagedList;
        }
    }
}
