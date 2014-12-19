using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService(ICatalogueContext context)
            : base(context)
        {
        }

        public Comment GetComment(int id)
        {
            return this.Context.Comments.Where(c => c.CommentID == id).FirstOrDefault();
        }

        public IEnumerable<Comment> GetByProduct(int productId)
        {
            return this.Context.Comments.Where(c => c.ProductID == productId);
        }

        public IEnumerable<Comment> GetByParent(int parentId)
        {
            return this.Context.Comments.Where(c => c.ParentCommentID == parentId);
        }

        public void Add(Comment comment)
        {
            this.Context.Comments.Add(comment);
            this.Context.SaveChanges();
        }

        public void Modify(Comment comment)
        {
            this.Context.Entry(comment).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void Remove(Comment comment)
        {
            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var comment = this.GetComment(id);
            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();
        }
    }
}
