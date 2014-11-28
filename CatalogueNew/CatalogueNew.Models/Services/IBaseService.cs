using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    interface IBaseService<T>
    {
        PagedList<T> GetItems(int? page);

        T Find(int? id);

        void Add(T category);

        void Modify(T category);

        void Remove(T category);

        void Remove(int id);
    }
}
