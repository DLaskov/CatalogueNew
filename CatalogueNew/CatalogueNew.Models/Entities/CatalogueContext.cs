using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace CatalogueNew.Models.Entities
{
    public class CatalogueContext : IdentityDbContext<User>, ICatalogueContext
    {
        public CatalogueContext()
            : base("DefaultConnection")
        {
#if DEBUG
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
#endif
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<LikeDislike> LikesDislikes { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTag> ProductsTags { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
    }
}
