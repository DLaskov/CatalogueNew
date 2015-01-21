using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(ICatalogueContext context)
            : base(context)
        {
        }

        public void ModifyUser(User user)
        {
            var checkEmail = this.Context.Users.FirstOrDefault(ur => ur.Email == user.Email && ur.Id != user.Id);

            if (checkEmail == null)
            {
                this.Context.Entry(user).State = EntityState.Modified;
                this.Context.SaveChanges();
            }
        }

        public User GetUserById(string id)
        {
            var user = this.Context.Users.Where(u => u.Id == id).FirstOrDefault();

            return user;
        }
    }
}
