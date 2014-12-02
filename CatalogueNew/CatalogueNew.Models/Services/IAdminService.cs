using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IAdminService
    {
        void Modify(User user);

        void ModifyUserRoles(User user, UserRole userRole);

        void Remove(User user);

        void DeleteUserRoles(User user);

        void AddUserRole(IdentityUserRole userRole);

        bool IsInRole(string userId, string role);

        User Find(string id);

        PagedList<User> GetUsers(int page);

        IEnumerable<User> GetAll();

        UserRole GetUserRoles(User user);
    }
}
