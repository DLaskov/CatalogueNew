using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetByProduct(int productId);

        IEnumerable<Comment> GetByParent(int parentId);

        void Add(Comment comment);

        void Modify(Comment comment);

        void Remove(int id);
    }
}
