using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
    }
}