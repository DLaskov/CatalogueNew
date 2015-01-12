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
using System.Threading;

namespace CatalogueNew.Web.Controllers
{
    public class RatingController : ApiController
    {
        private IRatingService ratingServices;

        public RatingController(IRatingService ratingServices)
        {
            this.ratingServices = ratingServices;
        }

        public int Get(int productID)
        {
            int totalRating = ratingServices.TotalRating(productID);

            return totalRating;
        }

        public Rating Post([FromBody]Rating rating)
        {
            rating.UserID = User.Identity.GetUserId();
            ratingServices.Add(rating);

            return rating;
        }
    }
}
