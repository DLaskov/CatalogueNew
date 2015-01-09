using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using CatalogueNew.Models.Entities;
using CatalogueNew.Web.Models;
using CatalogueNew.Models.Services;
using CatalogueNew.Models.Infrastructure;
using System.Data.Entity;
using System.Collections.Generic;

namespace CatalogueNew.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryServices;


        public CategoryController(ICategoryService categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Index(int page = 1)
        {
            PagedList<Category> pageItems = categoryServices.GetCategories(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var categoryListViewModels = new CategoryListViewModel()
            {
                Categories = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(categoryListViewModels);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Details(int id)
        {
            Category category = categoryServices.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel(category);
            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = model.Name
                };

                categoryServices.Add(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            Category category = categoryServices.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel(category);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryID = model.CategoryID,
                    Name = model.Name
                };

                categoryServices.Modify(category);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            Category category = categoryServices.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel(category);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryServices.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
