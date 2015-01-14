using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CatalogueNew.Models.Entities;

namespace CatalogueNew.Web.Models
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GenderType? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
    }
}