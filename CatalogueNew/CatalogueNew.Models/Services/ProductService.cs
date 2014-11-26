﻿using CatalogueNew.Models.Entities;
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

        public void Add(Product product)
        {
            if (product == null)
            {
                throw new NullReferenceException("Only non-nullable objects allowed!");
            }

            context.Products.Add(product);
        }

        public void Get(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetAt(int index)
        {
            return context.Products.Find(index);
        }
        public Category GetCategory(int id)
        {
            return context.Categories.Find(id);
        }
        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
