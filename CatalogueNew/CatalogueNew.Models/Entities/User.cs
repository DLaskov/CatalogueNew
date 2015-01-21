namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Web.Mvc;

    public class User : IdentityUser
    {

        public User()
        {
            Wishlists = new HashSet<Wishlist>();
        }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public override string UserName { get; set; }

        [MaxLength(30)]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required(ErrorMessage = "Fill E-Mail Address!")]
        public override string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public GenderType? Gender { get; set; }

        public virtual ICollection<Wishlist> Wishlists { get; set; }

    }

    public enum GenderType
    {
        Male = 0,
        Female = 1
    }
}
