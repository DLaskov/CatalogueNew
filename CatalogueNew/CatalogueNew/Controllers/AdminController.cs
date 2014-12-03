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
    public class AdminController : Controller
    {
        private IAdminService adminServices;
        private const string RedirectToUsers = "Users";

        public AdminController(IAdminService adminServices)
        {
            this.adminServices = adminServices;
        }

        public ActionResult Users(int? page)
        {
            PagedList<User> userPages = adminServices.GetUsersWhitRoles(page.GetValueOrDefault(1));

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
                User user = userViewModel.User;

                adminServices.Modify(user);
                adminServices.ModifyUserRoles(user, userRoles);

                return RedirectToAction(RedirectToUsers);
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

            return RedirectToAction(RedirectToUsers);
        }
    }
}