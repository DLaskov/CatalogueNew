using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using CatalogueNew.Models.Entities;
using CatalogueNew.Web.Models;
using CatalogueNew.Models.Services;
using CatalogueNew.Models.Infrastructure;

namespace CatalogueNew.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryServices;

        public CategoryController(ICategoryService categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        public ActionResult Index(int page = 1)
        {
            PagedList<Category> pageItems = categoryServices.GetCategories(page);
            var categoryListViewModel = new CategoryListViewModel(pageItems);

            return View(categoryListViewModel);
        }

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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryServices.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
