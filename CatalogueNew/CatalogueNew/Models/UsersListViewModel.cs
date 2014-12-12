using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class UsersListViewModel
    {
        public List<User> Users { get; set; }
        public PagingViewModel PagingViewModel { get; set; }
        public Dictionary<User, UserRole> UsersRoles { get; set; }
    }
}