using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(ICatalogueContext context)
            : base(context)
        {
        }

        public void Add(Rating rating)
        {
            var userRating = this.Context.Ratings.Where(ur => ur.UserID == rating.UserID && ur.ProductID == rating.ProductID).FirstOrDefault();

            if (userRating == null)
            {
                this.Context.Ratings.Add(rating);
                this.Context.SaveChanges();
            }
        }


        public Rating RatingByUserProduct(string userID, int productID)
        {
            var rating = this.Context.Ratings.Where(r => r.UserID == userID && r.ProductID == productID).FirstOrDefault();

            return rating;
        }

        public IQueryable<Rating> RatingsByProduct(int productID)
        {
            return this.Context.Ratings.Where(r => r.ProductID == productID);
        }
    }
}
