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
    public class ManufacturerService : BaseService<Manufacturer>, IManufacturerService
    {
        private readonly int pageSize = 5;

        public ManufacturerService(ICatalogueContext context)
            : base(context)
        {
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return this.Context.Manufacturers.ToList();
        }

        public Manufacturer Find(int id)
        {
            return this.Context.Manufacturers.Find(id);
        }

        public void Add(Manufacturer manufacturer)
        {
            this.Context.Manufacturers.Add(manufacturer);
            this.Context.SaveChanges();
        }

        public void Modify(Manufacturer manufacturer)
        {
            this.Context.Entry(manufacturer).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Remove(Manufacturer manufacturer)
        {
            this.Context.Manufacturers.Remove(manufacturer);
            this.Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var manufacturer = this.Find(id);
            this.Context.Manufacturers.Remove(manufacturer);
            this.Context.SaveChanges();
        }

        public PagedList<Manufacturer> GetManufacturers(int page)
        {
            var pagedList = new PagedList<Manufacturer>(this.Context.Manufacturers.OrderBy(c => c.Name), page, pageSize);
            return pagedList;
        }
    }
}
