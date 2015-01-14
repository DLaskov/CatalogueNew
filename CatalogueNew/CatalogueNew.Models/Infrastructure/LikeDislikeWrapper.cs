using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class LikeDislikeWrapper
    {
        public bool? IsLike { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public LikeDislikeWrapper(bool? isLike, int likesCount, int dislikesCount)
        {
            IsLike = isLike;
            LikesCount = likesCount;
            DislikesCount = dislikesCount;
        }
    }
}
