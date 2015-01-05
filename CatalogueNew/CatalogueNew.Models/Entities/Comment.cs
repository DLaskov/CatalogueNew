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
        [StringLength(1000)]
        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public string UserID { get; set; }

        public virtual User Users { get; set; }

        public int ProductID { get; set; }

        //public virtual Product Products { get; set; }

        public int? ParentCommentID { get; set; }

        //public virtual Comment ParentComment { get; set; }




    }
}
