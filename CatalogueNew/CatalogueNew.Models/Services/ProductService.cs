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
    public class ProductService : BaseService<Product>, IProductService
    {
        private const int pageSize = 9;

        public ProductService(ICatalogueContext context)
            : base(context)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            return this.Context.Products.ToList();
        }

        public Product Find(int id)
        {
            var queryProduct = (from product in this.Context.Products
                                    join category in this.Context.Categories on product.CategoryID
                                    equals category.CategoryID
                                    join manufacturer in this.Context.Manufacturers on product.ManufacturerID
                                    equals manufacturer.ManufacturerID
                                    where product.ProductID == id
                                    select new
                                    {
                                        Product = product,
                                        Category = category,
                                        Manufacturer = manufacturer
                                    }).FirstOrDefault();
            Product prod = new Product
            {
                ProductID = queryProduct.Product.ProductID,
                CategoryID = queryProduct.Product.CategoryID,
                ManufacturerID = queryProduct.Product.ManufacturerID,
                Name = queryProduct.Product.Name,
                Description = queryProduct.Product.Description,
                Year = queryProduct.Product.Year,
                Manufacturer = queryProduct.Manufacturer,
                Category = queryProduct.Category
            };        
            return prod;
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

        public PagedList<Product> GetProducts(int page)
        {
            var pagedList = new PagedList<Product>(this.Context.Products.OrderBy(c => c.Name), page, pageSize);
            return pagedList;
        }

        public PagedList<Product> GetProductsByManufacturer(int page, int manufacturerID)
        {
            var pagedList = new PagedList<Product>(this.Context
                .Products
                .Where(mn => mn.ManufacturerID == manufacturerID)
                .OrderBy(c => c.Name), page, pageSize);

            return pagedList;
        }
    }
}
