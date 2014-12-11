using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public class PagingViewModel
    {
        public int Count { get; set; }

        public int Page { get; set; }

        public string Path { get; set; }

        public PagingViewModel(int count, int page, string path)
        {
            this.Count = count;
            this.Page = page;
            this.Path = path;
        }
    }
}