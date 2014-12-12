using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
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

        public ProductController(ICategoryService categoryService,
            IManufacturerService manufacturerService, IProductService productService)
        {
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
            this.productService = productService;
        }

        public ActionResult ProductAdministration(int page = 1)
        {
            PagedList<Product> pageItems = productService.GetProducts(page);
            var productListViewModel = new ProductListViewModel(pageItems);

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
            model.Categories = categoryService.GetAll();
            model.Manufacturers = manufacturerService.GetAll();
            model.Product = new Product();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
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
                Categories = categoryService.GetAll(),
                Manufacturers = manufacturerService.GetAll()
            };

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            Product product = productService.Find(id);
            product.Name = model.Product.Name;
            product.CategoryID = model.Product.CategoryID;
            product.ManufacturerID = model.Product.ManufacturerID;
            product.Year = model.Product.Year;
            product.Description = model.Product.Description;

            productService.Modify(product);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index(int page = 1)
        {
            var pageItems = productService.GetProducts(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var productListViewModels = new ProductListViewModels()
            {
                Products = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(productListViewModels);
        }

        public ActionResult ProductsByManufacturer(int manufacturerID, int page = 1)
        {
            var pageItems = productService.GetProductsByManufacturer(page, manufacturerID);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "ProductsByManufacturer");

            var productListViewModels = new ProductListViewModels()
            {
                Products = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(productListViewModels);
        }
    }
}