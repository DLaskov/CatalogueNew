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
        Wishlist Find(int productID, string userID);

        void Add(Wishlist wishlist);

        void Remove(int id);
    }
}
