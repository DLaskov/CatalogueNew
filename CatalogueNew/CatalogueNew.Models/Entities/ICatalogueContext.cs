using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace CatalogueNew.Models.Entities
{
    public interface ICatalogueContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<LikeDislike> LikesDislikes { get; set; }
        DbSet<Manufacturer> Manufacturers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductTag> ProductsTags { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Wishlist> Wishlists { get; set; }
    }
}
