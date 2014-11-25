using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CatalogueNew.Models.Entities;
using CatalogueNew.Web.Models;
using CatalogueNew.Models.Services;

namespace CatalogueNew.Web.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryService categoryServices;

        public CategoryController()
        {
            this.categoryServices = DependencyResolver.Current.GetService<ICategoryService>();
        }

        public ActionResult Index(int? id)
        {
            var categories = categoryServices.Data.Categories;
            var paginatedCategories = categoryServices.GetCategories(categories, id);

            ViewBag.Pages = Math.Ceiling((double)categories.Count() / categoryServices.PageSize);
            ViewBag.Id = id;
            
            return View(paginatedCategories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryServices.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel()
            {
                CategoryID = category.CategoryID,
                Name = category.Name
            };

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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryServices.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel()
            {
                CategoryID = category.CategoryID,
                Name = category.Name
            };

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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryServices.Find(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new CategoryViewModel()
            {
                CategoryID = category.CategoryID,
                Name = category.Name
            };

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
