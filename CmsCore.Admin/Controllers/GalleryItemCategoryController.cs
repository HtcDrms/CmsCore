using CmsCore.Admin.Models;
using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Controllers
{
    public class GalleryItemCategoryController:BaseController
    {
        private readonly IGalleryItemCategoryService galleryItemCategoryService;
        public GalleryItemCategoryController(IGalleryItemCategoryService galleryItemCategoryService)
        {
            this.galleryItemCategoryService = galleryItemCategoryService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var galitemcvm = new GalleryItemCategoryViewModel();
            ViewBag.GalleryItemCategories = new SelectList(galleryItemCategoryService.GetGalleryItemCategories(), "Id", "Name");
            return View(galitemcvm);
        }

        [HttpPost]
        public ActionResult Create(GalleryItemCategoryViewModel vm)
        {
            GalleryItemCategory galitem = new GalleryItemCategory();
            galitem.Name = vm.Name;
            galitem.ParentCategoryId = vm.ParentCategoryId;
            galitem.Description = vm.Description;
            galitem.AddedBy = User.Identity.Name ?? "username";
            galitem.AddedDate = DateTime.Now;
            galitem.ModifiedBy = User.Identity.Name ?? "username";
            galitem.ModifiedDate = DateTime.Now;

            galleryItemCategoryService.CreateGalleryItemCategory(galitem);
            galleryItemCategoryService.SaveGalleryItemCategory();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            var galitem = galleryItemCategoryService.GetGalleryItemCategory(id);
            if (galitem != null)
            {

                GalleryItemCategoryViewModel linkCVM = new GalleryItemCategoryViewModel();
                linkCVM.Id = galitem.Id;
                linkCVM.Name = galitem.Name;
                linkCVM.Description = galitem.Description;
                linkCVM.ParentCategoryId = galitem.ParentCategoryId;
                ViewBag.GalleryItemCategories = new SelectList(galleryItemCategoryService.GetGalleryItemCategories().Where(c=>c.Id!=id), "Id", "Name", galitem.ParentCategoryId);
                return View(linkCVM);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]

        public ActionResult Edit(GalleryItemCategoryViewModel galCVM)
        {
            if (ModelState.IsValid)
            {
                GalleryItemCategory pc = galleryItemCategoryService.GetGalleryItemCategory(galCVM.Id);
                pc.Name = galCVM.Name;
                pc.Description = galCVM.Description;
                pc.ParentCategoryId = galCVM.ParentCategoryId;
                galleryItemCategoryService.UpdateGalleryItemCategory(pc);
                galleryItemCategoryService.SaveGalleryItemCategory();
                return RedirectToAction("Index");
            }
            ViewBag.LinkCategories = new SelectList(galleryItemCategoryService.GetGalleryItemCategories().Where(c => c.Id != galCVM.Id), "Id", "Name", galCVM.ParentCategoryId);
            return View(galCVM);
        }

        public ActionResult Delete(long id)
        {
            var galitem = galleryItemCategoryService.GetGalleryItemCategory(id);
            if (galitem != null)
            {
                var galitemval = galleryItemCategoryService.GetGalleryItemCategoriesById(galitem.Id);
                //foreach (var item in galitemval)
                //{
                //    galleryItemCategoryService.DeleteGalleryItemCategory(item.Id);
                //}
                galleryItemCategoryService.DeleteGalleryItemCategory(id);
                galleryItemCategoryService.SaveGalleryItemCategory();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"];
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedCategories = galleryItemCategoryService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);
            var result = from c in displayedCategories
                         select new[] {
                             c.Id.ToString(),
                             c.Name,
                             (c.ParentCategory==null?"(YOK)":c.ParentCategory.Name),
                             string.Empty };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = result.ToList()
            });
        }

    }
}
