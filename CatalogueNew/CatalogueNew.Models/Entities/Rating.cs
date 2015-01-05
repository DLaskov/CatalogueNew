namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Rating
    {
        [Key]
        public int RatingID { get; set; }

        public string UserID { get; set; }

        public int Value { get; set; }

        public int ProductID { get; set; }

        public virtual Product Products { get; set; }

        public virtual User Users { get; set; }
    }
}
