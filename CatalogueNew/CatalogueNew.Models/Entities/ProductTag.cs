namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbo.ProductsTags")]
    public class ProductTag
    {
        [Key]
        public int ProductTagID { get; set; }

        public int ProductID { get; set; }
        
        public int TagID { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
