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
        private const int pageSize = 9;

        public ProductService(ICatalogueContext context)
            : base(context)
        {
        }

        public IEnumerable<Product> All()
        {
            return this.Context.Products.ToList();
        }

        public Product Find(int id)
        {
            Product product = (from prod in this.Context.Products
                               where prod.ProductID == id
                               select prod).Include(x => x.Category)
                                .Include(x => x.Manufacturer).Include("Images").FirstOrDefault();
            return product;
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

        public PagedList<Product> GetProductsByManufacturer(int page, int manufacturerID)
        {
            var pagedList = new PagedList<Product>(this.Context
                .Products
                .Where(mn => mn.ManufacturerID == manufacturerID)
                .OrderBy(c => c.Name), page, pageSize);

            return pagedList;
        }

        public PagedList<Product> GetProducts(int page)
        {
            var products = this.Context.Products.OrderBy(c => c.Name).Include("Images");

            var pagedList = new PagedList<Product>(products, page, pageSize);
            return pagedList;
        }

        public PagedList<Product> GetProducts(int page, int? categoryId, int? manufacturerId)
        {
            if (categoryId != null && manufacturerId != null)
            {
                var products = this.Context.Products
                  .Where(x => x.ManufacturerID == manufacturerId && x.CategoryID == categoryId)
                  .Include("Images")
                  .OrderBy(c => c.Name);
                return new PagedList<Product>(products, page, pageSize);
            }
            else if (categoryId == null && manufacturerId != null)
            {
                var products = this.Context.Products
                  .Where(x => x.ManufacturerID == manufacturerId)
                  .Include("Images")
                  .OrderBy(c => c.Name);
                return new PagedList<Product>(products, page, pageSize);
            }
            else if (categoryId != null && manufacturerId == null)
            {
                var products = this.Context.Products
                  .Where(x => x.CategoryID == categoryId)
                  .Include("Images")
                  .OrderBy(c => c.Name);
                return new PagedList<Product>(products, page, pageSize);
            }
            else
            {
                var products = this.Context.Products.Include("Images").OrderBy(c => c.Name);
                return new PagedList<Product>(products, page, pageSize);
            }
        }

        public PagedList<Product> GetProducts(int page, string userID)
        {
            var products = (from prod in this.Context.Products
                            join wish in this.Context.Wishlists on prod.ProductID equals wish.ProductID
                            where userID == wish.UserID
                            orderby prod.Name
                            select prod).Include("Images");


            var pagedList = new PagedList<Product>(products, page, pageSize);
            return pagedList;
        }
    }
}
