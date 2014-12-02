using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required]
        public string Name { get; set; }

        public CategoryViewModel(Category category)
        {
            CategoryID = category.CategoryID;
            Name = category.Name;
        }
    }
}