﻿using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryService categoryService;
        private IManufacturerService manufacturerService;
        private IProductService productService;
        private IImageService imageService;

        public ProductController(ICategoryService categoryService,
            IManufacturerService manufacturerService, IProductService productService, IImageService imageService)
        {
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
            this.productService = productService;
            this.imageService = imageService;
        }

        public ActionResult ProductAdministration(int page = 1)
        {
            PagedList<Product> pageItems = productService.GetProducts(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "ProductAdministration");

            var productListViewModel = new ProductListViewModel()
            {
                Products = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(productListViewModel);
        }

        public ActionResult Details(int id)
        {
            Product product;

            product = productService.Find(id);
            if (product == null)
            {
                HttpContext.Response.StatusCode = 404;
                return View("_NotFound");
            }

            ProductViewModel model = new ProductViewModel()
            {
                Product = product
            };

            return View(model);
        }

        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.Categories = new SelectList(categoryService.GetAll(), "CategoryID", "Name");
            model.Manufacturers = new SelectList(manufacturerService.GetAll(), "ManufacturerID", "Name");
            model.Product = new Product();
            return View(model);
        }

        public JsonResult SaveUploadedFile(HttpPostedFileBase file)
        {
            var path = Path.Combine("~/Content/TempImages/", Path.GetTempFileName());
            if (file != null)
            {
                file.SaveAs(path);
            }
            return new JsonResult()
            {
                Data = new
                {
                    imgPath = path,
                    imgName = file.FileName,
                    MimeType = file.ContentType
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (User.IsInRole("Manager"))
            {
                if (!ModelState.IsValid)
                {
                    return View("Create");
                }

                Product product = new Product()
                {
                    Name = model.Product.Name,
                    Description = model.Product.Description,
                    CategoryID = model.Product.CategoryID,
                    ManufacturerID = model.Product.ManufacturerID,
                    Year = model.Product.Year
                };
                int productID = productService.Add(product);

                string[] imgAtributes = new String[3];
                for (int i = 1; i < 7; i++)
                {
                    PropertyInfo property = model.GetType().GetProperty("hidden" + i);
                    if (property.GetValue(model) != null)
                    {
                        imgAtributes = property.GetValue(model).ToString().Split(',');
                        byte[] imgValue = System.IO.File.ReadAllBytes(imgAtributes[0]);

                        Image productImage = new Image()
                        {
                            ProductID = productID,
                            Value = imgValue,
                            LastUpdated = DateTime.Now,
                            MimeType = imgAtributes[2],
                            ImageName = imgAtributes[1]
                        };
                        imageService.Add(productImage);
                        System.IO.File.Delete(imgAtributes[0]);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ActionName("RemoveImage")]
        public void RemoveImage(string value)
        {
            string[] imgAtributes = value.Split(',');
            System.IO.File.Delete(imgAtributes[0]);
        }

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
                Categories = new SelectList(categoryService.GetAll(), "CategoryID", "Name"),
                Manufacturers = new SelectList(manufacturerService.GetAll(), "ManufacturerID", "Name")
            };

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            if (User.IsInRole("Manager"))
            {
                Product product = productService.Find(id);
                product.Name = model.Product.Name;
                product.CategoryID = model.Product.CategoryID;
                product.ManufacturerID = model.Product.ManufacturerID;
                product.Year = model.Product.Year;
                product.Description = model.Product.Description;

                productService.Modify(product);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index(int page = 1)
        {
            var pageItems = productService.GetProducts(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var productListViewModels = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RenderProductsPartial", productListViewModels);
            }

            return View(productListViewModels);
        }

        public ActionResult ProductsByManufacturer(int id, int page = 1)
        {
            var pageItems = productService.GetProductsByManufacturer(page, id);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "ProductsByManufacturer", id);

            var productListViewModels = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

            return View(productListViewModels);
        }
    }
}