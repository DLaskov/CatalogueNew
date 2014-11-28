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
        private ICatalogueContext data;
        private readonly int pageSize = Int32.Parse(ConfigurationManager.AppSettings["PageSize"]);

        public CategoryService(ICatalogueContext data)
        {
            this.data = data;
        }

        public Category Find(int? id)
        {
            return data.Categories.Find(id);
        }

        public void Add(Category category)
        {
            data.Categories.Add(category);
            data.SaveChanges();
        }


        public void Modify(Category category)
        {
            data.Entry(category).State = EntityState.Modified;
            data.SaveChanges();
        }

        public void Remove(Category category)
        {
            data.Categories.Remove(category);
            data.SaveChanges();
        }

        public void Remove(int id)
        {
            var category = this.Find(id);
            data.Categories.Remove(category);
            data.SaveChanges();
        }

        public PagedList<Category> GetItems(int? page)
        {
            var pagedList = new PagedList<Category>(data.Categories.OrderBy(c => c.Name), page, pageSize);
            return pagedList;
        }
    }
}
