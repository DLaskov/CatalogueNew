using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ProductListViewModels
    {
        public List<Product> Products { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
    }
}