using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private UserManager<User> userManager;
        private IAuthService authService;

        public AuthController(UserManager<User> userManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInViewModel model)
        {
            bool passwordMatch = true;
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await userManager.FindAsync(model.UserName, model.Password);

            if (user == null)
            {
                user = await userManager.FindByEmailAsync(model.UserName);
                passwordMatch = await userManager.CheckPasswordAsync(user, model.Password);
            }
            if (user != null && passwordMatch)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            UserValidator(userManager);

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                LastName = model.LastName,
                Gender = model.Gender
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private async Task SignIn(User user)
        {
            var identity = await userManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);
            GetAuthenticationManager().SignIn(identity);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        private void UserValidator(UserManager<User> usermanager)
        {
            usermanager.UserValidator = new UserValidator<User>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        public ActionResult ChangePassword()
        {
            var userId = User.Identity.GetUserId();
            var user = authService.GetUserById(userId);

            var model = new ManageUserViewModel()
            {
                User = user
            };

            return PartialView("_ChangePasswordPartial", model);
        }

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Success ? "Well done!"
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (model.OldPassword == null || model.NewPassword == null || model.ConfirmPassword == null)
            {
                ModifyUserData(model);

                return RedirectToAction("Manage", new { Message = ManageMessageId.Success });
            }

            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await userManager.ChangePasswordAsync(model.User.Id, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        ModifyUserData(model);

                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
                    }
                }
            }
            else
            {
                ModelState state = ModelState["OldPassword"];

                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            return View(model);
        }

        private void ModifyUserData(ManageUserViewModel model)
        {
            var user = new User()
            {
                Id = model.User.Id,
                UserName = model.User.UserName,
                Email = model.User.Email,
                FirstName = model.User.FirstName,
                LastName = model.User.LastName,
                Gender = model.User.Gender,
                BirthDate = model.User.BirthDate,
                PasswordHash = model.User.PasswordHash,
                SecurityStamp = model.User.SecurityStamp,
            };

            authService.ModifyUser(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
                userManager = null;
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = userManager.FindById(User.Identity.GetUserId());

            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        public enum ManageMessageId
        {
            Success,
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
    }
}