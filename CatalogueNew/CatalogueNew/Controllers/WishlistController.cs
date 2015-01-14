using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CatalogueNew.Web.Controllers
{
    public class WishlistController : Controller
    {

        private WishlistService wishlistService;
        private ProductService productService;

        public WishlistController(WishlistService wishlistService, ProductService productService)
        {
            this.wishlistService = wishlistService;
            this.productService = productService;
        }

        public ActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return View();
            }

            string userID = User.Identity.GetUserId();
            var pageItems = productService.GetProducts(page, userID);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var productListViewModel = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RenderProductsPartial", productListViewModel);
            }

            return View(productListViewModel);
        }

        [HttpPost]
        public ActionResult RemoveFromWishlist(string data, string page)
        {
            int productID = Int32.Parse(data);
            string userID = User.Identity.GetUserId();
            wishlistService.Remove(productID, userID);

            int pageNumber = Int32.Parse(page);
            var pageItems = productService.GetProducts(pageNumber, userID);

            return Json(new { Page = pageItems.Items.Count() <= 0 ? pageItems.CurrentPage - 1 : pageItems.CurrentPage });
        }
    }
}