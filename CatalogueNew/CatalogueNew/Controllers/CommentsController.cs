using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private ICommentService commentsService;

        public CommentsController(ICommentService commentsService)
        {
            this.commentsService = commentsService;
        }

        public IEnumerable<Comment> GetByProduct(int productId)
        {
            var comments = commentsService.GetByProduct(productId);

            UTCToLocalTime(comments);

            return comments;
        }

        public IEnumerable<Comment> GetByParent(int parentId)
        {
            var comments = commentsService.GetByParent(parentId);

            UTCToLocalTime(comments);

            return comments;
        }

        private static void UTCToLocalTime(IEnumerable<Comment> comments)
        {
            foreach (var comment in comments)
            {
                comment.TimeStamp = TimeZone.CurrentTimeZone.ToLocalTime(comment.TimeStamp);
            }
        }

        public Comment Post([FromBody]Comment comment)
        {
            if (comment.Text != String.Empty)
            {
                comment.TimeStamp = DateTime.UtcNow;
                commentsService.Add(comment);
            }

            return comment;
        }

        public void Put([FromBody]Comment comment)
        {
            if (comment.Text != String.Empty)
            {
                comment.TimeStamp = DateTime.UtcNow;
                commentsService.Modify(comment);
            }
        }

        public void Delete(int commentId)
        {
            commentsService.Remove(commentId);
        }
    }
}