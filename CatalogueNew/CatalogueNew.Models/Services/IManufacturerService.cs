using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IManufacturerService
    {
        IEnumerable<Manufacturer> GetAll();

        Manufacturer Find(int? id);

        void Add(Manufacturer category);

        void Modify(Manufacturer category);

        void Remove(Manufacturer category);

        void Remove(int id);

        PagedList<Manufacturer> GetItems(int? page);
    }
}
