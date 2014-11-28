using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int PageCount { get; private set; }
        public int CurrentPage { get; private set; }

        public PagedList(IQueryable<T> source, int pageSize)
        {
            Items = source;
            PageCount = source.Count() / pageSize;
        }
        
        public PagedList<T> GetPage(int page, int pageSize) {
            if (page > PageCount)
            {
                throw new IndexOutOfRangeException();
            }
            CurrentPage = page;
            return new PagedList<T>(Items.Skip((CurrentPage - 1) * pageSize).Take(pageSize), pageSize);
        }
    }
}
