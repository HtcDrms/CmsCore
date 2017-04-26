using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakura.AspNetCore;

namespace CmsCore.Web.ViewComponents
{
    public class Posts : ViewComponent
    {
        private readonly IPostService postService;
        public Posts(IPostService postService)
        {
            this.postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string categoryNames)
        {
            int pageNumber = 1;
            var pageSize = 4;

            if (!String.IsNullOrEmpty(Request.Query["page"]))
            {
                pageNumber = Convert.ToInt32(Request.Query["page"]);
            }
            var items = GetItems(categoryNames).AsEnumerable();
            var pagedData = items.ToPagedList(pageSize, pageNumber);

            return View(pagedData);
        }
        public List<Post> GetItems(string categoryNames)
        {
            if (categoryNames != null)
            {
                return postService.GetPostsByCategoryNames(categoryNames,4).Where(w=>w.IsPublished==true).ToList();
            }
            else
            {
                return postService.GetPosts().Where(w => w.IsPublished == true).ToList();
            }
        }
    }
}
