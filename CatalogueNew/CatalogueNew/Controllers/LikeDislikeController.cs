using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services.Like;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using CatalogueNew.Web.Models;

namespace CatalogueNew.Web.Controllers
{
    public class LikeDislikeController : ApiController
    {
        private ILikeDislikeService likeServices;

        public LikeDislikeController(ILikeDislikeService likeServices)
        {
            this.likeServices = likeServices;
        }

        [Authorize]
        public async Task<LikesDislike> Post([FromBody]LikesDislike likeDislike)
        {
            var userID = User.Identity.GetUserId();
            likeDislike.UserID = userID;

            await likeServices.Add(likeDislike);

            return likeDislike;
        }

        public LikeDislikeViewModel Get(int productID)
        {
            var userID = User.Identity.GetUserId();
            var likesCount = likeServices.LikesCout(productID);
            var dislikesCount = likeServices.DislikesCout(productID);
            var likeDislike = likeServices.IsLike(productID, userID);
            LikeDislikeViewModel likesModel;

            if (likeDislike == null)
            {
                return likesModel = new LikeDislikeViewModel(null, likesCount, dislikesCount);
            }

            return likesModel = new LikeDislikeViewModel(likeDislike.IsLike, likesCount, dislikesCount);
        }
    }
}