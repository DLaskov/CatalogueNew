using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Manufacturers { get; set; }

        public List<String> ImagePaths = new List<string>();
    }
}