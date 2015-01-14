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
using CatalogueNew.Models.Infrastructure;

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

        public LikeDislikeWrapper Get(int productID)
        {
            var userID = User.Identity.GetUserId();
            LikeDislikeWrapper likeDislikeWrapper = likeServices.IsLikeDislikeCounts(productID, userID);

            return likeDislikeWrapper;
        }
    }
}