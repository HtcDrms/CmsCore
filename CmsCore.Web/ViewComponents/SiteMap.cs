using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class SiteMap : ViewComponent
    {
        private readonly IPageService pageService;
        public SiteMap(IPageService pageService)
        {
            this.pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? parentPageId)
        {
            var items = await GetItems(parentPageId);
            return View(items);           
        }
        private Task<List<Page>> GetItems(long? parentPageId)
        {
            List<Page> pages = pageService.GetPages(parentPageId).ToList();
            return Task.FromResult(pages);
        }
    }
}
