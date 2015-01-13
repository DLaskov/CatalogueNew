using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class LikeDislikeViewModel
    {
        public bool? IsLike { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public LikeDislikeViewModel(bool? isLike, int likesCount, int dislikesCount)
        {
            IsLike = isLike;
            LikesCount = likesCount;
            DislikesCount = dislikesCount;
        }
    }
}