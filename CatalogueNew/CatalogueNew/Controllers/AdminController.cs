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
            PagedList<User> userPages = adminServices.GetUsers(page.GetValueOrDefault(1));

            var usersListViewModels = new UsersListViewModels()
            {
                Users = userPages.Items.ToList(),
                Count = userPages.PageCount,
                Page = userPages.CurrentPage
            };

            return View(usersListViewModels);
        }

        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = adminServices.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            UserRole userRoles = adminServices.GetUserRoles(user);

            var userViewModel = new UserViewModels()
            {
                UserID = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                isAdmin = userRoles.IsAdmin,
                isManager = userRoles.IsManager,
                isModerator = userRoles.IsModerator
            };

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModels userViewModel, FormCollection form)
        {
            UserRole userRoles = GetUserRoles(form);

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Id = userViewModel.UserID,
                    UserName = userViewModel.UserName,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Email = userViewModel.Email,
                    Gender = userViewModel.Gender,
                };

                adminServices.Modify(user);
                adminServices.ModifyUserRoles(user, userRoles);

                return RedirectToAction(RedirectToUsers);
            }

            return View(userViewModel);
        }

        private static UserRole GetUserRoles(FormCollection form)
        {
            var isAdmin = false;
            var isManager = false;
            var isModerator = false;

            if (form["admin"] != null)
            {
                isAdmin = true;
            }

            if (form["manager"] != null)
            {
                isManager = true;
            }

            if (form["moderator"] != null)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = adminServices.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userViewModels = new UserViewModels()
            {
                UserID = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Gender = user.Gender
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

        [ChildActionOnly]
        public ActionResult UserRole(string id)
        {
            var model = new UserViewModels()
            {
                isAdmin = false,
                isManager = false,
                isModerator = false
            };

            if (adminServices.IsInRole(id, "Admin"))
            {
                model.isAdmin = true;
            }
            if (adminServices.IsInRole(id, "Manager"))
            {
                model.isManager = true;
            }
            if (adminServices.IsInRole(id, "Moderator"))
            {
                model.isModerator = true;
            }

            return PartialView("_IsInRolePartial", model);
        }
    }
}