using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class UserRole
    {
        public bool IsAdmin { get; set; }

        public bool IsManager { get; set; }

        public bool IsModerator { get; set; }
    }
}