﻿using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
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
    }
}