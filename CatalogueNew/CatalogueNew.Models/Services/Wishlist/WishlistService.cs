using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class WishlistService : BaseService, IWishlistService
    {
        private const int pageSize = 9;
        public WishlistService(ICatalogueContext context)
            : base(context)
        {
        }

        public Wishlist Find(int productID, string userID)
        {
            var wishlist = this.Context.
                Wishlists.
                Where(wl => wl.ProductID == productID && wl.UserID == userID).
                FirstOrDefault();

            return wishlist;
        }

        public void Add(Wishlist wishlist)
        {
            this.Context.Wishlists.Add(wishlist);
            this.Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var wishlist = this.Context.Wishlists.Find(id);
            this.Context.Wishlists.Remove(wishlist);
            this.Context.SaveChanges();
        }

        public void Remove(int productID, string userID)
        {
            var wishlist = Find(productID, userID);

            this.Context.Wishlists.Remove(wishlist);
            this.Context.SaveChanges();
        }
    }
}
