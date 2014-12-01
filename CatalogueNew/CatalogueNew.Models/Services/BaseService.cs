using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected ICatalogueContext Context { get; private set; }

        protected IDbSet<T> DbSet { get; set; }

        public BaseService(ICatalogueContext context)
        {
            this.Context = context;
            //this.DbSet = this.Context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
            this.Context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.Context.Entry(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this.DbSet.Find(id);
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }

    }
}