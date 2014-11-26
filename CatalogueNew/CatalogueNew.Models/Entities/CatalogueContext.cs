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

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Image> Images { get; set; }
        public virtual IDbSet<LikeDislike> LikesDislikes { get; set; }
        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<ProductTag> ProductsTags { get; set; }
        public virtual IDbSet<Rating> Ratings { get; set; }
        public virtual IDbSet<Tag> Tags { get; set; }
        public virtual IDbSet<Wishlist> Wishlists { get; set; }
    }
}
