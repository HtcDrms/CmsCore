using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class Gallery:ViewComponent
    {
        private readonly IGalleryService galleryService;
        public Gallery(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name, int count = 10, string template = "Default")
        {
            var items = await GetItems(name, count);
            return View(template, items);
        }
        private Task<List<CmsCore.Model.Entities.GalleryItem>> GetItems(string galleryName, int count)
        {
            List<CmsCore.Model.Entities.GalleryItem> galleries = galleryService.GetGalleryItems(galleryName, count).Where(w=>w.IsPublished==true).ToList();
            return Task.FromResult(galleries);
        }
    }
}
