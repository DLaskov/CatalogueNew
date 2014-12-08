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
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace CatalogueNew.Models.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private const int pageSize = 10;

        public AdminService(ICatalogueContext context)
            : base(context)
        {
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

            foreach (var role in userRoles)
            {
                this.Context.UserRoles.Remove(role);
            }
            this.Context.SaveChanges();
        }

        public PagedList<User> GetUsersWhitRoles(int page)
        {
            var pagedList = new PagedList<User>(this.Context.Users.OrderBy(c => c.UserName), page, pageSize);

            var usersRoles = (from us in pagedList.Items
                              join ur in this.Context.UserRoles
                              on us.Id equals ur.UserId
                              select ur).ToList();

            var usersWhitRoles = new Dictionary<User, UserRole>();

            foreach (var user in pagedList.Items)
            {
                UserRole userRole = new UserRole()
                {
                    IsAdmin = false,
                    IsManager = false,
                    IsModerator = false
                };

                foreach (var item in usersRoles)
                {

                    if (item.UserId == user.Id)
                    {
                        CheckForRoles(userRole, item);
                    }
                }

                usersWhitRoles.Add(user, userRole);
                pagedList.UsersRoles = usersWhitRoles;
            }

            return pagedList;
        }

        public Dictionary<User, UserRole> GetUserWhitRoles(string userId)
        {
            var user = this.Context.Users.Where(x => x.Id == userId);

            var userRoles = (from us in user
                             join ur in this.Context.UserRoles
                             on us.Id equals ur.UserId
                             select ur).ToList();

            var usersRoles = new Dictionary<User, UserRole>();

            UserRole userRole = new UserRole()
            {
                IsAdmin = false,
                IsManager = false,
                IsModerator = false
            };

            foreach (var item in userRoles)
            {
                CheckForRoles(userRole, item);
            }

            usersRoles.Add(user.FirstOrDefault(), userRole);

            return usersRoles;
        }

        private static void CheckForRoles(UserRole userRole, IdentityUserRole item)
        {
            if (item.RoleId == "1") //check for Admin
            {
                userRole.IsAdmin = true;
            }
            else if (item.RoleId == "2") //check for Manager
            {
                userRole.IsManager = true;
            }
            else if (item.RoleId == "3") //check for Moderator
            {
                userRole.IsModerator = true;
            }
        }
    }
}
