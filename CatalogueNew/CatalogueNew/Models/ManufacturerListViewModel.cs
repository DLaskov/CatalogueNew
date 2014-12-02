using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ManufacturerListViewModel
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }

        public ManufacturerListViewModel(PagedList<Manufacturer> pageItems)
        {
            Manufacturers = pageItems.Items.ToList();
            Count = pageItems.PageCount;
            Page = pageItems.CurrentPage;
        }
    }
}