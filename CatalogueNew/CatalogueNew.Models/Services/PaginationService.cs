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
    public class PaginationService<T> : IPaginationService<T> where T : class
    {
        private ICatalogueContext data;

        private readonly int pageSize = Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["PageSize"]);//3;

        public PaginationService()
            : this(new CatalogueContext())
        {
        }

        public PaginationService(ICatalogueContext data)
        {
            this.data = data;
        }

        public CategoryList GetCategories(int? page)
        {
            var categories = data.Categories;
            int pageNumber = page.GetValueOrDefault(1);
            var getCategories = categories.OrderBy(x => x.CategoryID).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var pages = Math.Ceiling((double)categories.Count() / pageSize);

            var categoryList = new CategoryList()
            {
                 Categories = getCategories,
                  Pages = pages
            };

            return categoryList;
        }
    }
}
