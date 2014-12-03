using CatalogueNew.Models.Entities;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; private set; }

        public int PageCount { get; private set; }

        public int CurrentPage { get; private set; }

        public Dictionary<User, UserRole> UsersRoles { get; set; }

        public PagedList(IQueryable<T> source, int page, int pageSize)
        {
            CurrentPage = page;
            PageCount = ((int)(Math.Ceiling((double)source.Count() / pageSize)));
            Items = source.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
        }

        public PagedList(IQueryable<T> source, int page, int pageSize, Dictionary<User, UserRole> usersRoles)
            : this(source, page, pageSize)
        {
            UsersRoles = usersRoles;
        }
    }
}
