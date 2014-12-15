using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (User.IsInRole("Manager"))
            {
                Product product = new Product()
                {
                    Name = model.Product.Name,
                    Description = model.Product.Description,
                    CategoryID = model.Product.CategoryID,
                    ManufacturerID = model.Product.ManufacturerID,
                    Year = model.Product.Year
                };
                productService.Add(product);
                SaveUploadedFile(product);
            }
            return RedirectToAction("Index", "Home");
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

        public ActionResult ProductsByManufacturer(int manufacturerID, int page = 1)
        {
            var pageItems = productService.GetProductsByManufacturer(page, manufacturerID);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "ProductsByManufacturer");

            var productListViewModels = new ProductListViewModel(pageItems.Items.ToList(), pagingViewModel);

            return View(productListViewModels);
        }

        public ActionResult SaveUploadedFile(Product product)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    byte[] binaryData;

                    using (BinaryReader reader = new BinaryReader(file.InputStream))
                    {
                        binaryData = reader.ReadBytes((int)file.InputStream.Length);
                    }

                    Image image = new Image
                    {
                        MimeType = file.ContentType,
                        LastUpdated = DateTime.Now,
                        Value = binaryData,
                    };

                    imageService.Add(image);
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}