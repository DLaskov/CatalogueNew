using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CatalogueNew.Models.Services.Like
{
    public class LikeDislikeService : BaseService, ILikeDislikeService
    {
        public LikeDislikeService(ICatalogueContext context)
            : base(context)
        {
        }

        public async Task Add(LikesDislike like)
        {
            var userLike = await this.Context.LikesDislikes.Where(l => l.UserID == like.UserID && l.ProductID == like.ProductID).FirstOrDefaultAsync();

            if (userLike == null)
            {
                this.Context.LikesDislikes.Add(like);
                this.Context.SaveChanges();
            }
            else
            {
                userLike.IsLike = like.IsLike;
                this.Context.Entry(userLike).State = EntityState.Modified;
                this.Context.SaveChanges();
            }
        }

        public List<LikesDislike> All(int productID)
        {
            return this.Context.LikesDislikes.Where(l => l.ProductID == productID).ToList();
        }

        public LikesDislike IsLike(int productID, string userID)
        {
            var likesDislikes = All(productID);

            return likesDislikes.Where(ld => ld.UserID == userID).FirstOrDefault();
        }

        public int LikesCout(int productID)
        {
            var likesDislikes = All(productID);
            var likes = likesDislikes.Where(ld => ld.IsLike == true).ToList();

            return likes.Count;
        }

        public int DislikesCout(int productID)
        {
            var likesDislikes = All(productID);
            var dislikes = likesDislikes.Where(ld => ld.IsLike == false).ToList();

            return dislikes.Count;
        }
    }
}
