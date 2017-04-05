using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Admin.Models;
using CmsCore.Model.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsCore.Admin.Controllers
{
    [Authorize(Roles = "ADMIN,GALLERY")]
    public class GalleryController : BaseController
    {
        private readonly IGalleryService galleryService;
        public GalleryController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(GalleryViewModel gallery)
        {
            if (ModelState.IsValid)
            {
                Gallery svm = new Gallery();
                svm.Name = gallery.Name;
                svm.IsPublished = gallery.IsPublished;
                svm.Id = gallery.Id;
                svm.ModifiedBy = User.Identity.Name ?? "username";
                svm.ModifiedDate = DateTime.Now;
                svm.AddedBy = User.Identity.Name ?? "username";
                svm.AddedDate = DateTime.Now;
                galleryService.CreateGallery(svm);
                galleryService.SaveGallery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            var gallery = galleryService.GetGallery(id);
            GalleryViewModel svm = new GalleryViewModel();
            svm.Name = gallery.Name;
            svm.IsPublished = gallery.IsPublished;
            svm.Id = gallery.Id;
            svm.ModifiedBy = gallery.ModifiedBy;
            svm.ModifiedDate = gallery.ModifiedDate;
            svm.AddedBy = gallery.AddedBy;
            svm.AddedDate = gallery.AddedDate;
            return View(svm);
        }

        [HttpPost]
        public IActionResult Edit(GalleryViewModel gallery)
        {
            if (ModelState.IsValid)
            {
                Gallery svm = galleryService.GetGallery(gallery.Id);
                svm.Name = gallery.Name;
                svm.IsPublished = gallery.IsPublished;
                svm.ModifiedBy = User.Identity.Name ?? "username";
                svm.ModifiedDate = DateTime.Now;
                svm.AddedBy = gallery.AddedBy;
                svm.AddedDate = gallery.AddedDate;
                galleryService.UpdateGallery(svm);
                galleryService.SaveGallery();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            galleryService.DeleteGallery(id);
            galleryService.SaveGallery();
            return RedirectToAction("Index");
        }


        public IActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPages = galleryService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);


            var result = from p in displayedPages
                         select new[] {
                             p.Id.ToString(),
                             p.Name.ToString(),
                             p.IsPublished.ToString(),
                             p.AddedBy.ToString(),
                             p.AddedDate.ToString(),
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
