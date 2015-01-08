using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Infrastructure
{
    public class CommentWrapper
    {
        public Comment Comment { get; set; }

        public List<CommentWrapper> Comments { get; set; }
    }
}
