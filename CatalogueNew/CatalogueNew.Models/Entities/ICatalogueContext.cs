using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CatalogueNew.Models.Entities
{
    public interface ICatalogueContext
    {
        IDbSet<Category> Categories { get; }
        IDbSet<Comment> Comments { get; }
        IDbSet<Image> Images { get; }
        IDbSet<LikeDislike> LikesDislikes { get; }
        IDbSet<Manufacturer> Manufacturers { get; }
        IDbSet<Product> Products { get; }
        IDbSet<ProductTag> ProductsTags { get; }
        IDbSet<Rating> Ratings { get; }
        IDbSet<Tag> Tags { get; }
        IDbSet<Wishlist> Wishlists { get; }
        IDbSet<User> Users { get; }

        int SaveChanges();
        DbEntityEntry Entry(Object entity);
    }
}
