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
            var pageNumber = 1; // Note that page number starts from 1 (not zero!)
            var pageSize = 4;
            
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
