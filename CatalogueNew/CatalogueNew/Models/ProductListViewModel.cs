using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ProductListViewModel
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }

        public ProductListViewModel(PagedList<Product> pageItems)
        {
            Products = pageItems.Items.ToList();
            Count = pageItems.PageCount;
            Page = pageItems.CurrentPage;
        }

        public ProductListViewModel()
        {

        }
    }
}