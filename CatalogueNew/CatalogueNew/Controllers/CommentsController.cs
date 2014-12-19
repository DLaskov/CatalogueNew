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

        public IEnumerable<Comment> Get(int productId)
        {
            return commentsService.GetByProduct(productId);
        }

        public IEnumerable<Comment> GetByParent(int parentId)
        {
            return commentsService.GetByParent(parentId);
        }

        //public Comment Get(int id)
        //{
        //    return commentsService.GetComment(id);
        //}

        public Comment Post([FromBody]Comment comment)
        {
            comment.TimeStamp = DateTime.UtcNow;

            commentsService.Add(comment);

            return comment;
        }

        public void Put([FromBody]Comment comment)
        {
            commentsService.Modify(comment);
        }

        public void Delete([FromBody]Comment comment)
        {
            commentsService.Remove(comment);
        }
    }
}