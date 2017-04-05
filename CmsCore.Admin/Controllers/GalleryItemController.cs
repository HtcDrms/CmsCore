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

        public GalleryItemController(IGalleryItemService galleryItemService)
        {
            this.galleryItemService = galleryItemService;
        }

        public IActionResult Index()
        {
            var galleryItems = galleryItemService.GetGalleryItems();
            return View(galleryItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GalleryItemViewModel galvm,IFormFile uploadedFilePhoto,IFormFile uploadedFileVideo)
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

                if (uploadedFilePhoto != null)
                {
                    if (Path.GetExtension(uploadedFilePhoto.FileName) == ".jpeg"
                    || Path.GetExtension(uploadedFilePhoto.FileName) == ".jpg"
                    || Path.GetExtension(uploadedFilePhoto.FileName) == ".gif"
                    || Path.GetExtension(uploadedFilePhoto.FileName) == ".png"
                     )
                    {
                        Random rnd = new Random();
                        gallery.Photo = rnd.Next(1, Int32.MaxValue) + uploadedFilePhoto.FileName;

                        string FilePath = ViewBag.UploadPath + "\\media\\galleryitem\\";
                        string dosyaYolu = gallery.Photo;
                        var yuklemeYeri = Path.Combine(FilePath + dosyaYolu);
                        try
                        {

                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eğer klasör yoksa oluştur
                                uploadedFilePhoto.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                uploadedFilePhoto.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }

                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }

                    if (uploadedFileVideo != null)
                    {
                        if (Path.GetExtension(uploadedFileVideo.FileName) == ".mp4"
                                || Path.GetExtension(uploadedFileVideo.FileName) == ".mpg"
                                || Path.GetExtension(uploadedFileVideo.FileName) == ".gif")
                        {
                            Random rnd = new Random();
                            gallery.Video = rnd.Next(1, Int32.MaxValue) + uploadedFileVideo.FileName;
                            string FilePath = ViewBag.UploadPath + "\\media\\galleryitem\\";
                            string dosyaYolu = gallery.Video;
                            var yuklemeYeri = Path.Combine(FilePath + dosyaYolu);
                            try
                            {
                                if (!Directory.Exists(FilePath))
                                {
                                    Directory.CreateDirectory(FilePath);//Eğer klasör yoksa oluştur
                                    uploadedFileVideo.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                                }
                                else
                                {
                                    uploadedFileVideo.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                                }
                            }
                            catch (Exception ex) { throw ex; }

                        }
                        else
                        {
                            ModelState.AddModelError("FileName", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                        }
                    }
                }
                galleryItemService.CreateGalleryItem(gallery);
                galleryItemService.SaveGalleryItem();
                return RedirectToAction("Index");
            }
            return null;
        }

        public IActionResult Edit(long id)
        {
            var gallery = galleryItemService.GetGalleryItem(id);
            GalleryItemViewModel galvm = new GalleryItemViewModel();
            galvm.Photo = gallery.Photo;
            galvm.IsPublished = gallery.IsPublished;
            galvm.Description = gallery.Description;
            galvm.Position = gallery.Position;
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
        public IActionResult Edit(GalleryItemViewModel galvm, IFormFile uploadedFilePhoto, IFormFile uploadedFileVideo)
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

                if (uploadedFilePhoto != null)
                {
                    if (gallery.Photo == uploadedFilePhoto.FileName)
                    {
                        gallery.Photo = galvm.Photo;
                    }
                    else if ((Path.GetExtension(uploadedFilePhoto.FileName) == ".jpeg"
                 || Path.GetExtension(uploadedFilePhoto.FileName) == ".jpg"
                 || Path.GetExtension(uploadedFilePhoto.FileName) == ".gif"
                 || Path.GetExtension(uploadedFilePhoto.FileName) == ".png") && gallery.Photo != uploadedFilePhoto.FileName
                  )
                    {
                        Random rnd = new Random();
                        gallery.Photo = rnd.Next(1, Int32.MaxValue) + uploadedFilePhoto.FileName;

                        string FilePath = ViewBag.UploadPath + "\\media\\galleryitem\\";
                        string dosyaYolu = gallery.Photo;
                        var yuklemeYeri = Path.Combine(FilePath + dosyaYolu);
                        try
                        {

                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eğer klasör yoksa oluştur
                                uploadedFilePhoto.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                uploadedFilePhoto.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }

                        }
                        catch (Exception) { }
                    }

                    if (gallery.Video == uploadedFileVideo.FileName)
                    {
                        gallery.Video = galvm.Video;
                    }
                    else if ((Path.GetExtension(uploadedFileVideo.FileName) == ".mp4"
                           || Path.GetExtension(uploadedFileVideo.FileName) == ".mpg"
                           || Path.GetExtension(uploadedFileVideo.FileName) == ".gif") && gallery.Video != galvm.Video)
                    {
                        Random rnd = new Random();
                        gallery.Video = rnd.Next(1, Int32.MaxValue) + uploadedFileVideo.FileName;
                        string FilePath = ViewBag.UploadPath + "\\media\\galleryitem\\";
                        string dosyaYolu = gallery.Video;
                        var yuklemeYeri = Path.Combine(FilePath + dosyaYolu);
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eğer klasör yoksa oluştur
                                uploadedFileVideo.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                uploadedFileVideo.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                        }
                        catch (Exception ex) { throw ex; }

                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantısı izin verilen uzantılardan olmalıdır.");
                    }

                }
                galleryItemService.UpdateGalleryItem(gallery);
                galleryItemService.SaveGalleryItem();
                return RedirectToAction("Index");
            }
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
                             ("<img src='"+ViewBag.AssetsUrl+"uploads/media/galleryitem/"+p.Photo.ToString()+"' width='100'>"),
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
