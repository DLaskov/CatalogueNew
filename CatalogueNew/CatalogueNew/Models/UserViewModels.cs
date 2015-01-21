using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class UserViewModels
    {
        public User User { get; set; }

        public UserRole UserRole { get; set; }

        public string Admin { get; set; }

        public string Manager { get; set; }

        public string Moderator { get; set; }
    }
}