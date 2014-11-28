using CatalogueNew.Models.Entities;
using System.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogueNew.Models.Infrastructure;
using System.Configuration;

namespace CatalogueNew.Models.Services
{
    public class ManufacturerService: IManufacturerService
    {
        private ICatalogueContext context;

        private readonly int pageSize = Int32.Parse(ConfigurationManager.AppSettings["PageSize"]);

        public ManufacturerService(ICatalogueContext context)
        {
            this.context = context;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return context.Manufacturers.ToList();
        }

        public Manufacturer Find(int? id)
        {
            return context.Manufacturers.Find(id);
        }

        public void Add(Manufacturer manufacturer)
        {
            context.Manufacturers.Add(manufacturer);
            context.SaveChanges();
        }

        public void Modify(Manufacturer manufacturer)
        {
            context.Entry(manufacturer).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(Manufacturer manufacturer)
        {
            context.Manufacturers.Remove(manufacturer);
            context.SaveChanges();
        }

        public void Remove(int id)
        { }

        public PagedList<Manufacturer> GetItems(int? page)
        {
            var pagedList = new PagedList<Manufacturer>(context.Manufacturers.OrderBy(c => c.Name), page, pageSize);
            return pagedList;
        }
    }
}
