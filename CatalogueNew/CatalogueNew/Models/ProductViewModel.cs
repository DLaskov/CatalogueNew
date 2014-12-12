using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }
    }
}