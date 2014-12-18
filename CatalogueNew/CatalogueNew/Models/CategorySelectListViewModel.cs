using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Models
{
    public class SelectListViewModel
    {
        public List<SelectListItem> SelectListManufacturers { get; set; }
        public List<SelectListItem> SelectListCategories { get; set; }

        public SelectListViewModel(List<SelectListItem> selectListManufacturers, List<SelectListItem> selectListCategories)
        {
            SelectListManufacturers = selectListManufacturers;
            SelectListCategories = selectListCategories;
        }
    }
}