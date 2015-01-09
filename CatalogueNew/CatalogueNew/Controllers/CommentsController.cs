using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using CatalogueNew.Models.Infrastructure;

namespace CatalogueNew.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private ICommentService commentsService;

        public CommentsController(ICommentService commentsService)
        {
            this.commentsService = commentsService;
        }

        public IEnumerable<CommentWrapper> Get(int productID)
        {
            var comments = commentsService.CommentsByProduct(productID);

            foreach (var comment in comments)
            {
                comment.ParentComment.TimeStamp = TimeZone.CurrentTimeZone.ToLocalTime(comment.ParentComment.TimeStamp);
            }

            return comments;
        }

        public Comment Post([FromBody]Comment comment)
        {
            if (comment.Text != String.Empty)
            {
                comment.UserID = User.Identity.GetUserId();
                comment.TimeStamp = DateTime.UtcNow;
                commentsService.Add(comment);
            }

            return comment;
        }

        [Authorize(Roles = "Admin, Manager, Moderator")]
        public void Put([FromBody]Comment comment)
        {
            if (comment.Text != String.Empty)
            {
                comment.TimeStamp = DateTime.UtcNow;
                commentsService.Modify(comment);
            }
        }

        [Authorize(Roles = "Admin, Manager, Moderator")]
        public void Delete(int commentId)
        {
            commentsService.Remove(commentId);
        }
    }
}