namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class User : IdentityUser
    {
        public enum GenderType
        {
            Male = 0,
            Female = 1
        }
        
        public User()
        {
            Wishlists = new HashSet<Wishlist>();
        }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public GenderType Gender { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; }

    }
}
