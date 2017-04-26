using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class ListPosts:ViewComponent
    {
        private readonly IPostService postService;
        public ListPosts(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string categoryNames = "", int count = 8)
        {
            var items = await GetItems(categoryNames, count);
            return View(items);
        }
        private Task<List<Post>> GetItems(string categoryNames, int count)
        {
            List<Post> posts = postService.GetPostsByCategoryNames(categoryNames, count).Where(w=>w.IsPublished==true).ToList();
            return Task.FromResult(posts);
        }
    }
}
