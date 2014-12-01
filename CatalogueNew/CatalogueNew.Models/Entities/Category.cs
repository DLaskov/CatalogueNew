namespace CatalogueNew.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentCategoryID { get; set; }

        //public virtual Category ParentCategory { get; set; }

        public ICollection<Product> products;

        public Category()
        {
            this.products = new HashSet<Product>();
        }

        public ICollection<Product> Products
        {
            get { return products; }
            set { products = value; }
        }
    }
}
