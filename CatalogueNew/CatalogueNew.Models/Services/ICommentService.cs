using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface ICommentService
    {
        IEnumerable<CommentWrapper> CommentsByProduct(int productID);

        void Add(Comment comment);

        void Modify(Comment comment);

        void Remove(int id);
    }
}
