using CmsCore.Admin.Models;
using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Controllers
{
    [Authorize(Roles = "ADMIN,GALLERY")]
    public class GalleryItemController:BaseController
    {
        private readonly IGalleryItemService galleryItemService;
        private readonly IGalleryItemCategoryService galleryItemCategoryService;

        public GalleryItemController(IGalleryItemService galleryItemService, IGalleryItemCategoryService galleryItemCategoryService)
        {
            this.galleryItemService = galleryItemService;
            this.galleryItemCategoryService = galleryItemCategoryService;
        }

        public IActionResult Index()
        {
            var galleryItems = galleryItemService.GetGalleryItems();
            return View(galleryItems);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = galleryItemCategoryService.GetGalleryItemCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(GalleryItemViewModel galvm)
        {
            if(ModelState.IsValid)
            {
                GalleryItem gallery = new GalleryItem();
                gallery.Title = galvm.Title;
                gallery.Description = galvm.Description;
                gallery.Position = galleryItemService.CountGalleryItem() + 1;
                gallery.AddedBy = User.Identity.Name??"username";
                gallery.AddedDate = DateTime.Now;
                gallery.ModifiedBy = User.Identity.Name??"username";
                gallery.ModifiedDate = DateTime.Now;
                gallery.Meta1 = galvm.Meta1;
                gallery.Photo = galvm.Photo;
                gallery.Video = galvm.Video;

                galleryItemService.CreateGalleryItem(gallery);
                galleryItemService.SaveGalleryItem();
                galleryItemService.UpdateCategories(gallery.Id, galvm.categoriesHidden);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = galleryItemCategoryService.GetGalleryItemCategories();
            return View(galvm);
        }

        public IActionResult Edit(long id)
        {
            GalleryItem gallery = galleryItemService.GetGalleryItem(id);
            ViewBag.CategoryList = galleryItemCategoryService.GetGalleryItemCategories();
            ViewBag.CheckList = gallery.GalleryItemGalleryItemCategories;
            

            GalleryItemViewModel galvm = new GalleryItemViewModel();
            galvm.Photo = gallery.Photo;
            galvm.IsPublished = gallery.IsPublished;
            galvm.Description = gallery.Description;
            galvm.Position = gallery.Position;
            galvm.Meta1 = gallery.Meta1;
            galvm.Title = gallery.Title;
            galvm.Video = gallery.Video;
            galvm.AddedBy = gallery.AddedBy;
            galvm.AddedDate = gallery.AddedDate;
            galvm.ModifiedBy = gallery.ModifiedBy;
            galvm.ModifiedDate = gallery.ModifiedDate;
            ViewBag.FileName = gallery.Photo ?? gallery.Video;
            return View(galvm);
        }

        [HttpPost]
        public IActionResult Edit(GalleryItemViewModel galvm)
        {
            if (ModelState.IsValid)
            {
                GalleryItem gallery = galleryItemService.GetGalleryItem(galvm.Id);
                gallery.Title = galvm.Title; 
                gallery.Description = galvm.Description; 
                gallery.Position = galleryItemService.CountGalleryItem() + 1;
                gallery.AddedBy = galvm.AddedBy;
                gallery.AddedDate = galvm.AddedDate;
                gallery.ModifiedBy = User.Identity.Name ?? "username";
                gallery.ModifiedDate = DateTime.Now;
                gallery.Video = galvm.Video;
                gallery.Photo = galvm.Photo;
                gallery.Meta1 = galvm.Meta1;
                
                galleryItemService.UpdateGalleryItem(gallery);
                galleryItemService.SaveGalleryItem();
                galleryItemService.UpdateCategories(gallery.Id, galvm.categoriesHidden);
                return RedirectToAction("Index");
            }
            var galleryR = galleryItemService.GetGalleryItem(galvm.Id);
            ViewBag.CategoryList = galleryItemCategoryService.GetGalleryItemCategories();
            ViewBag.CheckList = galleryR.GalleryItemGalleryItemCategories;
            return View(galvm);
        }

        public IActionResult Delete(long id)
        {
            galleryItemService.DeleteGalleryItem(id);
            galleryItemService.SaveGalleryItem();
            return RedirectToAction("Index", "GalleryItem");
        }

        public IActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPages = galleryItemService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);


            var result = from p in displayedPages
                         select new[] {
                             p.Id.ToString(),
                             ("<img src='"+p.Photo.ToString()+"' width='100'>"),
                             p.Title.ToString(),
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
