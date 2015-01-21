using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
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
            PagedList<User> userPages = adminServices.GetUsersWithRoles(page);
            var pagingViewModel = new PagingViewModel(userPages.PageCount, userPages.CurrentPage, "Users");

            var usersListViewModels = new UsersListViewModel()
            {
                Users = userPages.Items.ToList(),
                PagingViewModel = pagingViewModel,
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
            if (ModelState.IsValid)
            {
                UserRole userRoles = GetUserRoles(userViewModel);
                var user = adminServices.Find(userViewModel.User.Id);

                user.UserName = userViewModel.User.UserName;
                user.Email = userViewModel.User.Email;
                user.FirstName = userViewModel.User.FirstName;
                user.LastName = userViewModel.User.LastName;
                user.Gender = userViewModel.User.Gender;
                user.BirthDate = userViewModel.User.BirthDate;

                var isModify = adminServices.Modify(user);

                if (isModify)
                {
                    adminServices.ModifyUserRoles(user, userRoles);
                    return RedirectToAction(RedirectURL);
                }
            }

            var userModel = adminServices.GetUserWhitRoles(userViewModel.User.Id).FirstOrDefault();
            userViewModel.UserRole = userModel.Value;

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