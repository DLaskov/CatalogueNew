using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class CategoryServices : ICategoryServices
    {
        private CatalogueContext data;
        public CatalogueContext Data
        {
            get { return data; }
        }

        private const int pageSize = 3;
        int ICategoryServices.PageSize
        {
            get { return pageSize; }
        }

        public CategoryServices(CatalogueContext data)
        {
            this.data = data;
        }

        public IEnumerable<Category> GetCategories(IEnumerable<Category> categories, int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var getCategories = categories.OrderBy(x => x.CategoryID).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return getCategories;
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
    }
}
