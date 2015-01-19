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
            var ratingsByProduct = await this.All(rating.ProductID);
            var userRating = ratingsByProduct.Where(ur => ur.UserID == rating.UserID && ur.ProductID == rating.ProductID).FirstOrDefault();

            if (userRating == null)
            {
                this.Context.Ratings.Add(rating);
                await this.Context.SaveChangesAsync();
            }
        }

        private async Task<List<Rating>> All(int productID)
        {
            return await this.Context.Ratings.Where(r => r.ProductID == productID).ToListAsync();
        }

        public async Task<int> TotalRating(int productID)
        {
            var ratingsByProduct = await this.All(productID);

            if (ratingsByProduct.Count == 0)
            {
                return 0;
            }

            return (int)Math.Round(ratingsByProduct.Average(r => r.Value));

        }

        public async Task<int> UserRating(int productID, string userID)
        {
            var ratingsByProduct = await this.All(productID);

            return ratingsByProduct.Where(r => r.UserID == userID).Select(r => r.Value).FirstOrDefault();
        }
    }
}
