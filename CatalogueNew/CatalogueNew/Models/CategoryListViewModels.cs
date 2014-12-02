using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class CategoryListViewModels
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }

        public CategoryListViewModel(PagedList<Category> pageItems)
        {
            Categories = pageItems.Items.ToList();
            Count = pageItems.PageCount;
            Page = pageItems.CurrentPage;
        }
    }
}