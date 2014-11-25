using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;

namespace CatalogueNew.Models.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        Category Find(int id);

        void Add(Category category);

        void Modify(Category category);

        void Remove(Category category);

    }
}
