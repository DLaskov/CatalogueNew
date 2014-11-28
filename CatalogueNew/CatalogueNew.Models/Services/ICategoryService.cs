using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
namespace CatalogueNew.Models.Services
{
    public interface ICategoryService
    {
        PagedList<Category> GetItems(int? page);

        Category Find(int? id);

        void Add(Category category);

        void Modify(Category category);

        void Remove(Category category);

        void Remove(int id);
    }
}
