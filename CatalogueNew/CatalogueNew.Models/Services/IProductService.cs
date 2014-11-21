using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IProductService
    {
        void Add(Product product);

        void Get(Product product);

        Product GetAt(int index);

        void Remove(Product product);

        void RemoveAt(int index);
    }
}
