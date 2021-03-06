﻿using CatalogueNew.Models.Entities;
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
        public ProductService(ICatalogueContext context)
            : base(context)
        {

        }

        public int GetProductsPerPage(string id)
        {
            return this.Context.Users.Where(u => u.Id == id).Select(c => c.ProductsPerPage).Single();
        }

        public IEnumerable<Product> All()
        {
            return this.Context.Products.ToList();
        }

        public Product Find(int id)
        {
            Product product = (from prod in this.Context.Products
                               where prod.ProductID == id
                               select prod).Include(x => x.Category).Include(x => x.Manufacturer)
                               .Include(p => p.Images).Include(x => x.ProductsTags.Select( pt => pt.Tag)).FirstOrDefault();

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

        public PagedList<Product> GetProducts(int page, int pageSize)
        {
            var products = this.Context.Products.OrderBy(c => c.Name).Include("Images");

            var pagedList = new PagedList<Product>(products, page, pageSize);
            return pagedList;
        }

        public PagedList<Product> GetProducts(int page, int? categoryId, int? manufacturerId, int pageSize)
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

        public PagedList<Product> GetProducts(int page, string userID, int pageSize)
        {
            var products = (from prod in this.Context.Products
                            join wish in this.Context.Wishlists on prod.ProductID equals wish.ProductID
                            where userID == wish.UserID
                            orderby prod.Name
                            select prod).Include("Images");

            var pagedList = new PagedList<Product>(products, page, pageSize);
            return pagedList;
        }
        public PagedList<Product> GetProductsByTag(int page, int tagID, int pageSize)
        {
            var products = (from prod in this.Context.Products
                            join productsTags in this.Context.ProductsTags on prod.ProductID equals productsTags.ProductID
                            where tagID == productsTags.TagID
                            orderby prod.Name
                            select prod).Include("Images");

            var pagedList = new PagedList<Product>(products, page, pageSize);
            return pagedList;
        }
    }
}
