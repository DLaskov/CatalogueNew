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

        public string hidden1 { get; set; }
        public string hidden2 { get; set; }
        public string hidden3 { get; set; }
        public string hidden4 { get; set; }
        public string hidden5 { get; set; }
        public string hidden6 { get; set; }
    }
}