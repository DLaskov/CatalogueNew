using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace CatalogueNew.Web.Controllers
{
    public class RatingController : ApiController
    {
        private IRatingService ratingServices;

        public RatingController(IRatingService ratingServices)
        {
            this.ratingServices = ratingServices;
        }

        public async Task<RatingViewModel> Get(int productID)
        {
            var userID = User.Identity.GetUserId();

            int totalRating = await ratingServices.TotalRating(productID);
            int userRating = await ratingServices.UserRating(productID, userID);

            var ratingModel = new RatingViewModel(totalRating, userRating);

            return ratingModel;
        }

        [Authorize]
        public Task Post([FromBody]Rating rating)
        {
            rating.UserID = User.Identity.GetUserId();
            return ratingServices.Add(rating);
        }
    }
}
