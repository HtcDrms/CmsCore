using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Model.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsCore.Web.ViewComponents
{
    public class RelatedPages : ViewComponent
    {

        private readonly IPageService pageService;
        public RelatedPages(IPageService pageService)
        {
            this.pageService = pageService;
        }
        public async Task<IViewComponentResult> InvokeAsync(long Id)
        {
            var items = await RelatedPage(Id);
            return View(items);
        }

        public Task<List<Page>> RelatedPage(long Id)
        {
            return Task.FromResult(pageService.ChildPages(Id).Where(w=>w.IsPublished==true).ToList());
        }
    }
}
