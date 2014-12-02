using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Helpers
{ 
    public static class  PaginHelper
    {
        public static IHtmlString GetPages(this HtmlHelper htmlHelper, int count, int page)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            StringBuilder htmlString = new StringBuilder();

            for (int i = 1; i <= count; i++)
            {
                var url = urlHelper.Action("Index", new { page = i });

                if (page == i)
                {
                    htmlString.Append(String.Format("<li class='active'><a href='{0}'>{1}</a></li>", url, i));
                }
                else
                {
                    htmlString.Append(String.Format("<li><a href='{0}'>{1}</a></li>", url, i));
                }
            }

            return new HtmlString(htmlString.ToString());
        }
    }
}