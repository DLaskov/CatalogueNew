using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    class PagedList<T> where T : class
    {
        public IQueryable<T> Items { get; set; }
        public int PagesCount;
        int pageSize =
            Int32.TryParse(System.Configuration.ConfigurationSettings.AppSettings["PageSize"]);
        public void PagedList<T>(IDbSet<T> table)
        {
            Items = table.AsQueryable();
            PagesCount = (int)Math.Ceiling(table.Count() / pageSize);
        }

        public T GetPage(int? page)
        {
            

        }
    }
}
