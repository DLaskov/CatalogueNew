﻿using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CatalogueNew.Web.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryService categoryService;
        private IManufacturerService manufacturerService;
        private IProductService productService;
        private IImageService imageService;
        private IWishlistService wishlistService;
        private ITagService tagService;
        private int productsPerPage;

        public ProductController(ICategoryService categoryService, IManufacturerService manufacturerService,
            IProductService productService, IImageService imageService, IWishlistService wishlistService, ITagService tagService)
        {
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
            this.productService = productService;
            this.imageService = imageService;
            this.wishlistService = wishlistService;
            this.tagService = tagService;
            this.productsPerPage = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["PageSize"]);
        }

        protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);
            if (Request.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                if (HttpRuntime.Cache[ id + ".productsPerPage"] == null)
                {
                    productsPerPage = productService.GetProductsPerPage(User.Identity.GetUserId());
                    HttpRuntime.Cache.Add(id + ".productsPerPage", productsPerPage, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                }
                else
                {
                    productsPerPage = Int32.Parse(HttpRuntime.Cache[id + ".productsPerPage"].ToString());
                }
            }
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ProductAdministration(int page = 1)
        {
            PagedList<Product> pageItems = productService.GetProducts(page, productsPerPage);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "ProductAdministration");

            var productListViewModel = new ProductListViewModel()
            {
                Products = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(productListViewModel);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product product = productService.Find(id);

            if (product == null)
            {
                HttpContext.Response.StatusCode = 404;
                return View("_NotFound");
            }

            Wishlist wishlist = wishlistService.Find(id, User.Identity.GetUserId());

            if (wishlist == null)
            {
                wishlist = new Wishlist()
                {
                    WishlistID = 0
                };
            }

            ProductViewModel model = new ProductViewModel()
            {
                Product = product,
                Wishlist = wishlist
            };

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            productService.Remove(id);
            return RedirectToAction("ProductAdministration");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.Categories = new SelectList(categoryService.All(), "CategoryID", "Name");
            model.Manufacturers = new SelectList(manufacturerService.All(), "ManufacturerID", "Name");
            model.Product = new Product();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Create(ProductViewModel model)
        {
            var images = new List<Image>();

            var path = Server.MapPath("~/Images/TempImages/");

            if (model.FileAttributesCollection != null)
            {
                foreach (var attributes in model.FileAttributesCollection)
                {
                    string[] FileAttributes = attributes.Split('\\');

                    images.Add(new Image()
                    {
                        Value = FileAttributes[0].GetFileData(path),
                        LastUpdated = DateTime.UtcNow,
                        ImageName = FileAttributes[1],
                        MimeType = FileAttributes[2]
                    });
                    try
                    {
                        System.IO.File.Delete(path + FileAttributes[0]);
                    }
                    catch
                    {
                        System.IO.File.Delete(path + FileAttributes[0]);
                    }
                }
            }

            foreach (var image in images)
            {
                image.Value = image.ResizeImage();
            }

            model.Product.Images = images;

            productService.Add(model.Product);

            return RedirectToAction("Index", "Product");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            Product product = productService.Find(id);
            if (product == null)
            {
                HttpContext.Response.StatusCode = 404;
                return View("_NotFound");
            }

            ProductViewModel model = new ProductViewModel()
            {
                Product = product,
                Categories = new SelectList(categoryService.All(), "CategoryID", "Name"),
                Manufacturers = new SelectList(manufacturerService.All(), "ManufacturerID", "Name")
            };

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            if (ModelState.IsValid)
            {

                Product product = productService.Find(id);
                product.Name = model.Product.Name;
                product.CategoryID = model.Product.CategoryID;
                product.ManufacturerID = model.Product.ManufacturerID;
                product.Year = model.Product.Year;
                product.Description = model.Product.Description;
                var images = new List<Image>();

                var path = Server.MapPath("~/Images/TempImages/");

                if (model.FileAttributesCollection != null)
                {
                    foreach (var attributes in model.FileAttributesCollection)
                    {
                        string[] FileAttributes = attributes.Split('\\');

                        images.Add(new Image()
                        {
                            Value = FileAttributes[0].GetFileData(path),
                            LastUpdated = DateTime.UtcNow,
                            ImageName = FileAttributes[1],
                            MimeType = FileAttributes[2]
                        });
                        try
                        {
                            System.IO.File.Delete(path + FileAttributes[0]);
                        }
                        catch
                        {
                            System.IO.File.Delete(path + FileAttributes[0]);
                        }
                    }
                }

                foreach (var image in images)
                {
                    image.Value = image.ResizeImage();
                    product.Images.Add(image);
                }

                productService.Modify(product);
            }
            return RedirectToAction("ProductAdministration", "Product");
        }

        [AllowAnonymous]
        public ActionResult Index(int? category, int? manufacturer, int page = 1)
        {
            var pageItems = productService.GetProducts(page, category, manufacturer, productsPerPage);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var productListViewModels = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RenderProductsPartial", productListViewModels);
            }

            return View(productListViewModels);
        }

        [AllowAnonymous]
        public ActionResult ManufacturersCategoriesSelectList()
        {
            var manufacturersList = new List<SelectListItem>();
            var categoriesList = new List<SelectListItem>();
            var manufacturers = manufacturerService.All();
            var categories = categoryService.All();

            foreach (var manufacturer in manufacturers)
            {
                manufacturersList.Add(new SelectListItem()
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.ManufacturerID.ToString()
                });
            }

            foreach (var category in categories)
            {
                categoriesList.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.CategoryID.ToString()
                });
            }

            var selectListItems = new SelectListViewModel(manufacturersList, categoriesList);

            return PartialView("_SelectListPartial", selectListItems);
        }

        [Authorize(Roles = "Manager")]
        public JsonResult SaveUploadedFile(HttpPostedFileBase file)
        {
            bool isSavedSuccessfully = true;
            string uniqueFileName = String.Empty;

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = Server.MapPath("~/Images/TempImages/");
                    string pathString = originalDirectory.ToString();
                    bool isExists = Directory.Exists(pathString);

                    if (!isExists)
                    {
                        System.IO.Directory.CreateDirectory(pathString);
                    }
                    uniqueFileName = Guid.NewGuid().ToString();
                    var path = string.Format("{0}\\{1}", pathString, uniqueFileName);

                    file.SaveAs(path);
                }
            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }

            if (isSavedSuccessfully)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        UniqueName = uniqueFileName,
                        ImgName = file.FileName,
                        MimeType = file.ContentType
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public void RemoveImage(string value)
        {
            string[] FileAttributes = value.Split('\\');
            var path = Server.MapPath("~/Images/TempImages/");
            try
            {
                System.IO.File.Delete(path + FileAttributes[0]);
            }
            catch
            {
                System.IO.File.Delete(path + FileAttributes[0]);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public void RemoveImageById(string value)
        {
            imageService.Remove(Int32.Parse(value));
        }

        [HttpPost]
        public JsonResult AddToWishlist(string data)
        {
            var wishlist = new Wishlist
            {
                ProductID = Int32.Parse(data),
                UserID = User.Identity.GetUserId()
            };

            wishlistService.Add(wishlist);

            return Json(new WishlistViewModel { Message = wishlist.WishlistID.ToString() }, JsonRequestBehavior.AllowGet);
        }

        public void RemoveFromWishlist(string data)
        {
            wishlistService.Remove(Int32.Parse(data));
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddTag(string tagName, string productID)
        {
            tagService.Add(tagName, Int32.Parse(productID));
            Tag currentTag = tagService.Find(tagName.ToLower());
            Response.StatusCode = 200;
            if (User.IsInRole("Manager"))
            {
                return new JsonResult() { Data = new { isInRole = "yes", tagID = currentTag.TagID } };
            }
            else
            {
                return new JsonResult() { Data = new { isInRole = "no", tagID = currentTag.TagID } };
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public void RemoveTag(string tagID, string productID)
        {
            tagService.Remove(Int32.Parse(tagID), Int32.Parse(productID));
        }

        public ActionResult Tag(string name, int page = 1)
        {
            if (name != null)
            {
                if (page <= 0)
                {
                    page = 1;
                }
                string nameLower = name.ToLower();
                Tag tag = tagService.Find(nameLower);
                if (tag == null)
                {
                    Response.StatusCode = 404;
                    return View();
                }
                var pageItems = productService.GetProductsByTag(page, tag.TagID, productsPerPage);
                var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Tag", nameLower);

                var productListViewModel = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_RenderProductsPartial", productListViewModel);
                }

                ViewBag.TagName = nameLower;
                return View(productListViewModel);
            }
            else
            {
                Response.StatusCode = 404;
                return View();
            }
        }
    }
}