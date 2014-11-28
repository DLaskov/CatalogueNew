using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class PaginationList<T> where T : class
    {
        public T Item { get; private set; }

        public PaginationList(T item)
        {
            Item = item;
        }
    }
}
