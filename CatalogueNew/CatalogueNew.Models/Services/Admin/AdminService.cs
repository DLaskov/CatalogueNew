using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace CatalogueNew.Models.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private const int pageSize = 10;
        private UserManager<User> userManager;
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

        public bool Modify(User user)
        {
            var checkUser = this.Context.Users.Where(ur => ur.UserName == user.UserName && ur.Id != user.Id).FirstOrDefault();
            var checkEmail = this.Context.Users.Where(ur => ur.Email == user.Email && ur.Id != user.Id).FirstOrDefault();

            if(checkUser == null && checkEmail == null)
            {
                this.Context.Entry(user).State = EntityState.Modified;
                this.Context.SaveChanges();
                return true;
            }
            return false;
   
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
            var comments = this.Context.Comments.Where(c => c.UserID == user.Id).OrderByDescending(c => c.CommentID).ToList();
            foreach (Comment comment in comments)
            {
                this.Context.Database.ExecuteSqlCommand("usp_deleteComments @id = {0}", comment.CommentID);
            }
            this.Context.Database.ExecuteSqlCommand("usp_deleteRatingsWishlistsLikesByUserID @UserID = {0}", user.Id);
            this.Context.SaveChanges();
            User tempUser = userManager.FindById(user.Id);
            userManager.Delete(tempUser);
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

        public PagedList<User> GetUsersWithRoles(int page)
        {
            var pagedList = new PagedList<User>(this.Context.Users.OrderBy(c => c.UserName), page, pageSize);

            var usersRoles = (from us in pagedList.Items
                              join ur in this.Context.UserRoles
                              on us.Id equals ur.UserId
                              select ur).ToList();

            var usersWithRoles = new Dictionary<User, UserRole>();

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

                usersWithRoles.Add(user, userRole);
            }
            pagedList.UsersRoles = usersWithRoles;
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
