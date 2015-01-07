using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class WishlistController : Controller
    {

        private WishlistService wishlistService;

        public WishlistController(WishlistService wishlistService)
        {
            this.wishlistService = wishlistService;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Index(int page = 1)
        //{
        //    var pageItems = wishlistService.GetWishlists(page);
        //    var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

        //    var wishlistListViewModel = new WishlistViewModel(pageItems.Items.ToList(), pagingViewModel);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_RenderProductsPartial", productListViewModels);
        //    }

        //    return View(wishlistListViewModel);
        //}


    }
}