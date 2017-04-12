using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var items = await Post();
            return View(items);
        }
        public Task<List<Post>> Post()
        {
            return Task.FromResult(postService.GetPosts().ToList());
        }
    }
}
