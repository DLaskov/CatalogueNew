using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services.Base;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace CatalogueNew.Models.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private const int pageSize = 10;
        UserManager<User> userManager;

        public AdminService(ICatalogueContext context, UserManager<User> userManager)
            : base(context)
        {
            this.userManager = userManager;
        }

        public IEnumerable<User> GetAll()
        {
            return this.Context.Users.ToList();
        }

        public User Find(string id)
        {
            return this.Context.Users.Find(id);
        }

        public void Modify(User user)
        {
            this.Context.Entry(user).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void ModifyUserRoles(User user, UserRole userRole)
        {
            this.DeleteUserRoles(user);

            if (userRole.IsAdmin)
            {
                var admin = new IdentityUserRole()
                {
                    RoleId = "1",
                    UserId = user.Id
                };

                this.AddUserRole(admin);
            }

            if (userRole.IsManager)
            {
                var manager = new IdentityUserRole()
                {
                    RoleId = "2",
                    UserId = user.Id
                };

                this.AddUserRole(manager);
            }

            if (userRole.IsModerator)
            {
                var moderator = new IdentityUserRole()
                {
                    RoleId = "3",
                    UserId = user.Id
                };

                this.AddUserRole(moderator);
            }
        }

        public void Remove(User user)
        {
            this.Context.Users.Remove(user);
            this.Context.SaveChanges();
        }

        public void AddUserRole(IdentityUserRole userRole)
        {
            this.Context.UserRoles.Add(userRole);
            this.Context.SaveChanges();
        }

        public void DeleteUserRoles(User user)
        {
            var userRoles = this.Context.UserRoles.Where(x => x.UserId == user.Id);

            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    this.Context.UserRoles.Remove(role);
                }
                this.Context.SaveChanges();
            }
        }

        public PagedList<User> GetUsersWhitRoles(int page)
        {
            var users = this.Context.Users.OrderBy(c => c.UserName);
            var pagedList = new PagedList<User>(users, page, pageSize);
            //var userManager = new UserManager<User>(new UserStore<User>(new CatalogueContext()));
            var usersRoles = new Dictionary<User, UserRole>();
            UserRole userRole;

            foreach (var user in pagedList.Items)
            {
                userRole = CheckRoles(user, userManager);

                usersRoles.Add(user, userRole);
                pagedList.UsersRoles = usersRoles;
            }

            return pagedList;
        }

        public Dictionary<User, UserRole> GetUserWhitRoles(string userId)
        {
            var user = this.Context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var userManager = new UserManager<User>(new UserStore<User>(new CatalogueContext()));
            var usersRoles = new Dictionary<User, UserRole>();
            var userRole = CheckRoles(user, userManager);

            usersRoles.Add(user, userRole);

            return usersRoles;
        }

        private static UserRole CheckRoles(User user, UserManager<User> userManager)
        {

            var userRole = new UserRole()
            {
                IsAdmin = false,
                IsManager = false,
                IsModerator = false
            };

            List<string> roles = userManager.GetRoles(user.Id).ToList();

            if (roles.Count != 0)
            {
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        userRole.IsAdmin = true;
                    }
                    else if (role == "Manager")
                    {
                        userRole.IsManager = true;
                    }
                    else if (role == "Moderator")
                    {
                        userRole.IsModerator = true;
                    }
                }
            }
            return userRole;
        }
    }
}
