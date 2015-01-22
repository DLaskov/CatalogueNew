using System;
using System.Collections.Generic;
namespace CatalogueNew.Models.Services
{
    public interface ICommonService<T>
     where T : class
    {
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        T Find(int id);
        void Modify(T entity);
    }
}
