using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class PagesList<T>
    {
        public IEnumerable<T> PageItems { get; set; }

        public double Pages { get; set; }
    }
}
