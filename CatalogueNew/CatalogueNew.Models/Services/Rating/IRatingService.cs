using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface IRatingService
    {
        Task Add(Rating rating);

        Task<int> TotalRating(int productID);

        Task<int> UserRating(int productID, string userID);
    }
}
