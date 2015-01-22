using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IManufacturerService : ICommonService<Manufacturer>
    {
        PagedList<Manufacturer> GetManufacturers(int page);
        IEnumerable<Manufacturer> All();
    }
}
