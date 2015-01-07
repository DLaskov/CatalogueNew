using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
namespace CatalogueNew.Models.Services
{
    public interface ICategoryService : ICommonService<Category>
    {
        PagedList<Category> GetCategories(int page);
    }
}
