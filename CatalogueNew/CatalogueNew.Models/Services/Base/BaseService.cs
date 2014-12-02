﻿using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services.Base
{
    public class BaseService
    {
        protected ICatalogueContext Context { get; private set; }

        public BaseService(ICatalogueContext context)
        {
            Context = context;
        }
    }
}