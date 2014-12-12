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

        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.Categories = categoryService.GetAll();
            model.Manufacturers = manufacturerService.GetAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryID = model.CategoryID,
                ManufacturerID = model.ManufactureID,
                Year = model.Year
            };
            productService.Add(product);
            return View();
        }

        public ActionResult Index(int page = 1)
        {
            var pageItems = productService.GetProducts(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var productListViewModels = new ProductListViewModel()
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

            var productListViewModels = new ProductListViewModel()
            {
                Products = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(productListViewModels);
        }
    }
}