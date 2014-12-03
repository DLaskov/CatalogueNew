using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class UsersListViewModels
    {
        public int Count { get; set; }

        public int? Page { get; set; }

        public Dictionary<User, UserRole> UsersRoles { get; set; }
    }
}