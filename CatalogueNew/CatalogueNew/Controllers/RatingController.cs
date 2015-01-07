using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CatalogueNew.Web.Controllers
{
    public class RatingController : ApiController
    {
        private IRatingService ratingServices;

        public RatingController(IRatingService ratingServices)
        {
            this.ratingServices = ratingServices;
        }

        public Rating Get(string userID, int productID)
        {
            Rating rating = ratingServices.RatingByUserProduct(userID, productID);

            return rating;
        }

        public Rating Post([FromBody]Rating rating)
        {
            ratingServices.Add(rating);

            return rating;
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
