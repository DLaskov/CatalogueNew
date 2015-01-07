using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(ICatalogueContext context)
            : base(context)
        {
        }

        public IEnumerable<Comment> GetByProduct(int productId)
        {
            return this.Context.Comments
                .Where(c => c.ProductID == productId && c.ParentCommentID == null)
                .Include("Users")
                .Where(coment => this.Context.Users.Any(user => user.Id == coment.UserID));
        }

        public IEnumerable<Comment> GetByParent(int parentId)
        {
            return this.Context.Comments
                .Where(c => c.ParentCommentID == parentId)
                .Include("Users")
                .Where(coment => this.Context.Users.Any(user => user.Id == coment.UserID));
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

        public void Remove(int id)
        {
            this.Context.Database.ExecuteSqlCommand("usp_deleteComments @id = {0}", id);
        }
    }
}
