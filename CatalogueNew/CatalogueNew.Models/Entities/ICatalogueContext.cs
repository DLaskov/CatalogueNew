using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CatalogueNew.Models.Entities
{
    public interface ICatalogueContext
    {
        IDbSet<Category> Categories { get; set; }
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Image> Images { get; set; }
        IDbSet<LikeDislike> LikesDislikes { get; set; }
        IDbSet<Manufacturer> Manufacturers { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<ProductTag> ProductsTags { get; set; }
        IDbSet<Rating> Ratings { get; set; }
        IDbSet<Tag> Tags { get; set; }
        IDbSet<Wishlist> Wishlists { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<IdentityUserRole> UserRoles { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}
