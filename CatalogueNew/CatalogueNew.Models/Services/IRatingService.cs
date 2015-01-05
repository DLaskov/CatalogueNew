using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IRatingService
    {
        void Add(Rating rating);

        Rating RatingByUserProduct(string userID, int productID);
    }
}
