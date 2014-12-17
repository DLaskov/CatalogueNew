namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Product
    {
        public Product()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
            LikesDislikes = new HashSet<LikeDislike>();
            ProductsTags = new HashSet<ProductTag>();
            Ratings = new HashSet<Rating>();
        }

        [Key]
        public int? ProductID { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(160)]
        public string Name { get; set; }

        public int ManufacturerID { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        [Required]
        [DisplayName("Year of product")]
        public int? Year { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<LikeDislike> LikesDislikes { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<ProductTag> ProductsTags { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
