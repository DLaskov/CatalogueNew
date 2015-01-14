using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class RatingViewModel
    {
        public int TotalRating { get; set; }

        public int UserRating { get; set; }

        public RatingViewModel(int totalRating, int userRating)
        {
            TotalRating = totalRating;
            UserRating = userRating;
        }
    }
}