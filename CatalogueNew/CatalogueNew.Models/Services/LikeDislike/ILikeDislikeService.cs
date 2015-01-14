using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services.Like
{
    public interface ILikeDislikeService
    {
        Task Add(LikesDislike like);

        LikeDislikeWrapper IsLikeDislikeCounts(int productID, string userID);
    }
}
