using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class PopularPosts: ViewComponent
    {
        private readonly IPostService postService;
        public PopularPosts(IPostService postService)
        {
            this.postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int total,long id)
        {
            var items = await Popular(total,id);
            return View(items);
        }
        public Task<List<Post>> Popular(int total, long id)
        {
            return Task.FromResult(postService.PopulerPost(total,id).ToList());
        }

    }
}
