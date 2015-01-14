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
        Task<IEnumerable<CommentWrapper>> CommentsByProduct(int productID);

        Task Add(Comment comment);

        Task Modify(Comment comment);

        Task Remove(int id);
    }
}
