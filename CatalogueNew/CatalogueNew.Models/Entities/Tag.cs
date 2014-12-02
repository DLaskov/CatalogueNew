namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TagID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
