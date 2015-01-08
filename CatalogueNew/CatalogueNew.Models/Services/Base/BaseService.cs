using CatalogueNew.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueNew.Models.Services
{
    public class BaseService
    {
        protected ICatalogueContext Context { get; private set; }

        public BaseService(ICatalogueContext context)
        {
            this.Context = context;
        }
    }
}