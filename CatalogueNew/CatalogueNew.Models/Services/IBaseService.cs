using System;
namespace CatalogueNew.Models.Services
{
    interface IBaseService<T>
     where T : class
    {
        void Add(T entity);
        System.Linq.IQueryable<T> All();
        void Delete(int id);
        void Delete(T entity);
        T GetById(int id);
        void Update(T entity);
    }
}
