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

        public PagedList<User> GetUsers(int page)
        {
            var pagedList = new PagedList<User>(this.Context.Users.OrderBy(c => c.UserName), page, pageSize);
            return pagedList;
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

        public bool IsInRole(string userId, string role)
        {
            var userRoles = this.Context.UserRoles.Where(x => x.UserId == userId);

            var userRole = (from ur in this.Context.UserRoles.Where(x => x.UserId == userId)
                            join r in this.Context.Roles.Where(x => x.Name == role)
                                on ur.RoleId equals r.Id
                            select r.Name).FirstOrDefault();

            if (userRole == role)
            {
                return true;
            }

            return false;
        }

        public UserRole GetUserRoles(User user)
        {
            var userRoles = this.Context.UserRoles.Where(x => x.UserId == user.Id);

            bool isAdmin = false;
            bool isManager = false;
            bool isModerator = false;

            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    if (role.RoleId == "1")
                    {
                        isAdmin = true;
                    }
                    if (role.RoleId == "2")
                    {
                        isManager = true;
                    }
                    if (role.RoleId == "3")
                    {
                        isModerator = true;
                    }
                }
            }

            var roles = new UserRole()
                {
                    IsAdmin = isAdmin,
                    IsManager = isManager,
                    IsModerator = isModerator
                };

            return roles;
        }
    }
}
