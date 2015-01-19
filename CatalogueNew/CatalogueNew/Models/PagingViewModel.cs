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

        public int PagesDisplayed { get; private set; }

        public int FirstPage { get; private set; }

        public int Id { get; set; }

        public string TagName { get; set; }

        public PagingViewModel(int count, int page, string path)
        {
            this.Count = count;
            this.Page = page;
            this.Path = path;

            CalculatePages();
        }

        public PagingViewModel(int count, int page, string path, int id)
            : this(count, page, path)
        {
            this.Id = id;

            CalculatePages();
        }

        public PagingViewModel(int count, int page, string path, string tagName)
            : this(count, page, path)
        {
            this.TagName = tagName;

            CalculatePages();
        }

        private void CalculatePages()
        {
            PagesDisplayed = 4;

            if (PagesDisplayed < Count)
            {
                FirstPage = Page - PagesDisplayed / 2;

                if (FirstPage < 1)
                {
                    FirstPage = 1;
                }

                if (Page + 1 >= Count)
                {
                    FirstPage = Count - PagesDisplayed;
                }
            }
            else
            {
                PagesDisplayed = Count - 1;
                FirstPage = 1;
            }

            if (Count <= 1)
            {
                PagesDisplayed = 0;
                FirstPage = 1;
            }
        }
    }
}