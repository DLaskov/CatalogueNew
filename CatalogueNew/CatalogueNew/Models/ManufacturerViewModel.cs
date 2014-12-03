using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ManufacturerViewModel
    {
        public int ManufacturerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ManufacturerViewModel(Manufacturer manufacturer)
        {
            ManufacturerID = manufacturer.ManufacturerID;
            Name = manufacturer.Name;
            Description = manufacturer.Description;
        }

        public ManufacturerViewModel()
        {
        }
    }
}