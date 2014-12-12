using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Models
{
    public class CategorySelectListViewModel
    {
        public string Name { get; set; }
        public List<SelectListItem> SelectListItems { get; set; }

        public CategorySelectListViewModel(List<SelectListItem> selectListItems)
        {
            SelectListItems = selectListItems;
        }
    }
}