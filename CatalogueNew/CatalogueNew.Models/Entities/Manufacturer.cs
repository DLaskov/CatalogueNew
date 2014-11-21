namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Manufacturer
    {
        [Key]
        public int ManufacturerID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
