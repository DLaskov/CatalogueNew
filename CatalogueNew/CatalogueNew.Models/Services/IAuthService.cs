using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IAuthService
    {
        void ModifyUser(User user);

        User GetUserById(string id);

        void SaveChanges();
    }
}
