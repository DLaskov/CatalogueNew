using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CatalogueNew.Models.Services
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService(ICatalogueContext context)
            : base(context)
        {
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

        public IEnumerable<CommentWrapper> CommentsByProduct(int productID)
        {
            var commentsByProduct = this.Context.Comments
                .Where(c => c.ProductID == productID)
                .Include("Users")
                .Where(coment => this.Context.Users.Any(user => user.Id == coment.UserID))
                .ToList();

            var commentsWrapperList = new List<CommentWrapper>();

            foreach (var comment in commentsByProduct.ToList())
            {
                if (comment.ParentCommentID == null)
                {
                    commentsWrapperList.Add(new CommentWrapper()
                    {
                        ParentComment = comment,
                        ChildrenComments = new List<CommentWrapper>(0)
                    });
                    commentsByProduct.Remove(comment);
                }
            }

            return SetChildren(commentsWrapperList, commentsByProduct);
        }

        private List<CommentWrapper> SetChildren(List<CommentWrapper> commentsWrapper, List<Comment> commentsByProduct)
        {
            if (commentsByProduct.Count == 0)
            {
                return commentsWrapper;
            }

            foreach (var commentWrapper in commentsWrapper)
            {
                var children = commentsByProduct.Where(x => x.ParentCommentID == commentWrapper.ParentComment.CommentID).ToList();

                foreach (var child in children)
                {
                    commentWrapper.ChildrenComments.Add(new CommentWrapper()
                    {
                        ParentComment = child,
                        ChildrenComments = new List<CommentWrapper>(0)
                    });
                    commentsByProduct.Remove(child);
                }
                SetChildren(commentWrapper.ChildrenComments, commentsByProduct);
            }

            return commentsWrapper;
        }
    }
}
