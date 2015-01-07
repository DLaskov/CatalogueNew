using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IWishlistService
    {
        PagedList<Wishlist> GetWishlists(int page);

        Wishlist Find(int productID, string userID);

        void Add(Wishlist wishlist);

        void Modify(Wishlist wishlist);

        void Remove(Wishlist wishlist);

        void Remove(int id);

        IEnumerable<Wishlist> GetAll();
    }
}
