namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }

        [Required]
        public int ProductID { get; set; }

        public int? ParentCommentID { get; set; }

        public virtual Product Products { get; set; }

        public virtual User User { get; set; }
    }
}
