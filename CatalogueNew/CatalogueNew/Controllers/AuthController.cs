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
    public class AuthController : Controller
    {
        private UserManager<User> userManager;
        private IAuthService authService;
        private readonly string ChangePasswordSuccess = "Your profile has been updated and password has been changed.";
        private readonly string ModifyUserSuccess = "Your profile has been updated.";
        private readonly string WrongPassword = "Wrong password.";

        public AuthController(UserManager<User> userManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
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

                if (model.ReturnUrl == null)
                {
                    return RedirectToAction("Index", "Product");
                }

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Product");
            }

            return returnUrl;
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
                return RedirectToAction("Index", "Product");
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
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        [Authorize]
        public ActionResult Manage(string message)
        {
            var user = authService.GetUserById(User.Identity.GetUserId());

            var model = new ManageUserViewModel(user, message);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            var user = authService.GetUserById(User.Identity.GetUserId());

            user.Email = model.User.Email;
            user.FirstName = model.User.FirstName;
            user.LastName = model.User.LastName;
            user.BirthDate = model.User.BirthDate;
            user.Gender = model.User.Gender;
            user.ProductsPerPage = model.User.ProductsPerPage;

            if (ModelState.IsValid)
            {
                if (model.OldPassword == null || model.NewPassword == null || model.ConfirmPassword == null)
                {
                    authService.ModifyUser(user);

                    return RedirectToAction("Manage", new { Message = ModifyUserSuccess });
                }

                IdentityResult result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                var passHash = userManager.PasswordHasher.HashPassword(model.NewPassword);

                if (result.Succeeded)
                {
                    user.PasswordHash = passHash;

                    authService.ModifyUser(user);

                    return RedirectToAction("Manage", new { Message = ChangePasswordSuccess });
                }
                else
                {
                    return RedirectToAction("Manage", new { Message = WrongPassword });
                }
            }

            return View(model);
        }
    }
}