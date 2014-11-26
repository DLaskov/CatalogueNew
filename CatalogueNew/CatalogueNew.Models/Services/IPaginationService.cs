using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatalogueNew.Models.Services
{
    public interface IPaginationService<T>
    {
        CategoryList GetCategories(int? page);
    }
}
