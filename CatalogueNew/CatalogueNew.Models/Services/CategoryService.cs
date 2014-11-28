using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class CategoryService : ICategoryService
    {
        private ICatalogueContext context;
        private readonly int pageSize = 3;

        public CategoryService(ICatalogueContext context)
        {
            this.context = context;
        }

        public Category Find(int? id)
        {
            return context.Categories.Find(id);
        }

        public void Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }


        public void Modify(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var category = this.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public CategoryList GetCategories(int page)
        {

            var categoryList = new CategoryList()
            {
                Categories = pagedList.Items,
                Count = pagedList.PageCount
            };

            return categoryList;
        }
    }
}
