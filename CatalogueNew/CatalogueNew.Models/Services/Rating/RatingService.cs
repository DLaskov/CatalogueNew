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

        public int TotalRating(int productID)
        {
            double totalRating = 0;

            var ratings = this.Context.Ratings.Where(r => r.ProductID == productID).Select(r => r.Value).ToList();

            if (ratings.Count == 0)
            {
                return 0;
            }

            foreach (var rating in ratings)
            {
                totalRating += rating;
            }

            totalRating = Math.Round(totalRating / ratings.Count);

            return (int)totalRating;
        }
    }
}
