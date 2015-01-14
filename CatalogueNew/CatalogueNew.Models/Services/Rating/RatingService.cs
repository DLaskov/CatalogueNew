using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CatalogueNew.Models.Services
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(ICatalogueContext context)
            : base(context)
        {
        }

        public async Task Add(Rating rating)
        {
            var userRating = await this.Context.Ratings.Where(ur => ur.UserID == rating.UserID && ur.ProductID == rating.ProductID).FirstOrDefaultAsync();

            if (userRating == null)
            {
                this.Context.Ratings.Add(rating);
                await this.Context.SaveChangesAsync();
            }
        }

        public async Task<int> TotalRating(int productID)
        {
            var rating = await this.Context.Ratings.Where(r => r.ProductID == productID).AverageAsync(r => r.Value);

            return (int)rating;
        }

        public async Task<int> UserRating(int productID, string userID)
        {
            var result = await this.Context.Ratings.Where(r => r.ProductID == productID && r.UserID == userID).Select(r => r.Value).FirstOrDefaultAsync();

            return result;
        }
    }
}
