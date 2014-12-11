using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminService adminServices;
        private const string RedirectURL = "Users";

        public AdminController(IAdminService adminServices)
        {
            this.adminServices = adminServices;
        }

        public ActionResult Users(int page = 1)
        {
            PagedList<User> userPages = adminServices.GetUsersWhitRoles(page);

            var usersListViewModels = new UsersListViewModels()
            {
                Count = userPages.PageCount,
                Page = userPages.CurrentPage,
                UsersRoles = userPages.UsersRoles
            };

            return View(usersListViewModels);
        }

        public ActionResult EditUser(string id)
        {
            var user = adminServices.GetUserWhitRoles(id).FirstOrDefault();

            var userViewModel = new UserViewModels()
            {
                User = user.Key,
                UserRole = user.Value
            };

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModels userViewModel)
        {
            UserRole userRoles = GetUserRoles(userViewModel);

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Id = userViewModel.User.Id,
                    UserName = userViewModel.User.UserName,
                    Email = userViewModel.User.Email,
                    FirstName = userViewModel.User.FirstName,
                    LastName = userViewModel.User.LastName,
                    Gender = userViewModel.User.Gender,
                    BirthDate = userViewModel.User.BirthDate,
                    PasswordHash = userViewModel.User.PasswordHash,
                    SecurityStamp = userViewModel.User.SecurityStamp
                };

                adminServices.Modify(user);
                adminServices.ModifyUserRoles(user, userRoles);

                return RedirectToAction(RedirectURL);
            }

            return View(userViewModel);
        }

        private static UserRole GetUserRoles(UserViewModels userViewModel)
        {
            var isAdmin = false;
            var isManager = false;
            var isModerator = false;

            if (userViewModel.Admin != null)
            {
                isAdmin = true;
            }

            if (userViewModel.Manager != null)
            {
                isManager = true;
            }

            if (userViewModel.Moderator != null)
            {
                isModerator = true;
            }

            UserRole roles = new UserRole()
            {
                IsAdmin = isAdmin,
                IsManager = isManager,
                IsModerator = isModerator
            };

            return roles;
        }

        public ActionResult DeleteUser(string id)
        {
            var user = adminServices.GetUserWhitRoles(id).FirstOrDefault();

            var userViewModels = new UserViewModels()
            {
                User = user.Key,
                UserRole = user.Value
            };

            return View(userViewModels);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string id)
        {
            User user = adminServices.Find(id);
            adminServices.Remove(user);

            return RedirectToAction(RedirectURL);
        }
    }
}