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
        public List<Manufacturer> Manufacturers { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
    }
}