using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ProductListViewModel
    {
        private int rows = 0;
        public int Rows
        {
            get { return rows; }
        }

        public List<Product> Products { get; set; }
        public PagingViewModel PagingViewModel { get; set; }

        public ProductListViewModel()
        {
        }

        public ProductListViewModel(List<Product> products, PagingViewModel pagingViewModel)
        {
            this.Products = products;
            this.PagingViewModel = pagingViewModel;

            if (products.Count > 2)
            {
                rows = Products.Count / 3 == 0 ? Products.Count : Products.Count / 3;
            }
        }
    }
}