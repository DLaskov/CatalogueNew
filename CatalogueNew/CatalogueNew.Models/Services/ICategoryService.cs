using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
namespace CatalogueNew.Models.Services
{
    public interface ICategoryService
    {
        
        int PageSize { get; }

        IEnumerable<Category> GetCategories(IEnumerable<Category> categories, int? id);

        Category Find(int? id);

        void Add(Category category);

        void Modify(Category category);

        void Remove(Category category);

        void Remove(int id);

    }
}
