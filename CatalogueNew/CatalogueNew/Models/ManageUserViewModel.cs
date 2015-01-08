using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class ManageUserViewModel
    {
        private User user;

        public ManageUserViewModel()
        {
        }

        public ManageUserViewModel(User user, string manageMessage)
        {
            this.user = user;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.BirthDate = user.BirthDate;
            this.Gender = user.Gender;
            this.ManageMessage = manageMessage;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [MaxLength(30)]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required(ErrorMessage="Fill E-Mail Address!")]
        public string Email { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Fill your first name.")]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Select gender.")]
        public GenderType? Gender { get; set; }

        public string ManageMessage { get; set; }
    }
}