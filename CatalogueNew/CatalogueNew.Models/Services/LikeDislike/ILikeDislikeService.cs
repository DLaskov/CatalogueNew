using CatalogueNew.Models.Entities;
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

        LikesDislike IsLike(int productID, string userID);

        int LikesCout(int productID);

        int DislikesCout(int productID);
    }
}
