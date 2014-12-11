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
        [Required]
        public string Name { get; set; }

        public int ManufactureID { get; set; }

        public int CategoryID { get; set; }

        public int Year { get; set; }

        [Required]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Manufacturer> Manufacturers { get; set; }
    }
}