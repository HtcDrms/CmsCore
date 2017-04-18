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
    public class Posts: ViewComponent
    {
        private readonly IPostService postService;
        public Posts(IPostService postService)
        {
            this.postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int pageNumber = 1;            
            var pageSize = 4;

            if (!String.IsNullOrEmpty(Request.Query["page"]))
            {
                pageNumber = Convert.ToInt32(Request.Query["page"]);
            }
            var items = GetItems().AsEnumerable();
            var pagedData = items.ToPagedList(pageSize, pageNumber);

            return View(pagedData);
        }
        public List<Post> GetItems()
        {
            return postService.GetPosts().ToList();
        }
    }
}
