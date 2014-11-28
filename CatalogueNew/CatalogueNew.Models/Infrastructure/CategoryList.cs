using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class CategoryList
    {
        public IEnumerable<Category> Categories { get; set; }
        public int Count { get; set; }
    }
}
