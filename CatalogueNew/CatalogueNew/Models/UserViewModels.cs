using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class UserViewModels
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public User.GenderType? Gender { get; set; }

        public bool isAdmin { get; set; }

        public bool isManager { get; set; }

        public bool isModerator { get; set; }
    }
}