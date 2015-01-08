using CatalogueNew.Models.Entities;
using CatalogueNew.Models.Infrastructure;
using CatalogueNew.Models.Services;
using CatalogueNew.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CatalogueNew.Web.Controllers
{
    public class ManufacturerController : Controller
    {
        private IManufacturerService manufacturerServices;

        public ManufacturerController(IManufacturerService manufacturerServices)
        {
            this.manufacturerServices = manufacturerServices;
        }

        public ActionResult Index(int page = 1)
        {
            PagedList<Manufacturer> pageItems = manufacturerServices.GetManufacturers(page);
            var pagingViewModel = new PagingViewModel(pageItems.PageCount, pageItems.CurrentPage, "Index");

            var manufacturerListViewModels = new ManufacturerListViewModel()
            {
                Manufacturers = pageItems.Items.ToList(),
                PagingViewModel = pagingViewModel
            };

            return View(manufacturerListViewModels);
        }

        public ActionResult LayoutManufacturers()
        {
            var manufacturers = manufacturerServices.All();

            return PartialView("_LayoutManufacturersPartial", manufacturers);
        }

        public ActionResult Details(int id)
        {
            Manufacturer manufacturer = manufacturerServices.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            var model = new ManufacturerViewModel(manufacturer);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManufacturerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = new Manufacturer()
                {
                    Name = model.Name,
                    Description = model.Description
                };

                manufacturerServices.Add(manufacturer);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            Manufacturer manufacturer = manufacturerServices.Find(id);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            var model = new ManufacturerViewModel(manufacturer);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManufacturerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = new Manufacturer()
                {
                    ManufacturerID = model.ManufacturerID,
                    Name = model.Name,
                    Description = model.Description
                };
                manufacturerServices.Modify(manufacturer);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            Manufacturer manufacturer = manufacturerServices.Find(id);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }

            var model = new ManufacturerViewModel(manufacturer);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            manufacturerServices.Remove(id);
            return RedirectToAction("Index");
        }
    }
}