using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace CatalogueNew.Models.Services
{
    public interface IAdminService
    {
        bool Modify(User user);

        void ModifyUserRoles(User user, UserRole userRole);

        void Remove(User user);

        void DeleteUserRoles(User user);

        void AddUserRole(IdentityUserRole userRole);

        User Find(string id);

        IEnumerable<User> GetAll();

        PagedList<User> GetUsersWithRoles(int page);

        Dictionary<User, UserRole> GetUserWhitRoles(string userId);
    }
}
