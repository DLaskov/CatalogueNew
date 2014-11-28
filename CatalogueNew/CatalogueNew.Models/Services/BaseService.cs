using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    class BaseService
    {
        private IDbSet<T> table;
        private ICatalogueContext context;

        public T Items;
        public int Pages;

        private static int pageSize = 
            Int32.Parse(System.Configuration.ConfigurationSettings.AppSettings["PageSize"]);

        public PagedList(IDbSet<T> table, ICatalogueContext context)
        {
            this.table = table;
            this.context = context;
        }

        public T Find(int? id)
        {
            return table.Find(id);
        }

        public void Add(T item)
        {
            table.Add(item);
            context.SaveChanges();
        }

        public void Modify(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(T item)
        {
            table.Remove(item);
            context.SaveChanges();
        }

        public void RemoveById(int? id)
        {
            T item = this.Find(id);
            table.Remove(item);
            context.SaveChanges();
        }

    }
}
