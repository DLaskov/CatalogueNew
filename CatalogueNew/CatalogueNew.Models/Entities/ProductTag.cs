namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class ProductTag
    {
        [Key]
        public int ProductTagID { get; set; }

        public int ProductID { get; set; }

        public int TagID { get; set; }

        public virtual Product Products { get; set; }

        public virtual Tag Tags { get; set; }
    }
}
