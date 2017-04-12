using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class LatestPostsOnRightSection : ViewComponent
    {
        private readonly IPostService postService;
        public LatestPostsOnRightSection(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryNames = "", int count = 8, long id = -1)
        {

            var items = await GetItems(categoryNames, count,id);

            return View(items);
        }
        private Task<List<Post>> GetItems(string categoryNames, int count,long id)
        {
            List<Post> posts = postService.GetPostsByCategoryNames(categoryNames, count ,id).ToList();
            return Task.FromResult(posts);
        }
    }
}
