using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Web.Models;
using CmsCore.Model.Entities;

namespace CmsCore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPageService pageService;
        private readonly IPostService postService;
        public HomeController(IPageService pageService, IPostService postService)
        {
            this.pageService = pageService;
            this.postService = postService;
        }
        public IActionResult Index(string slug)
        {
            if (slug != "")
            {

                // Do redirect operations

                // get page by slug

                // if page is null get post by slug

                // if post is null get product by slug

                // if product is null display 404 not found page

            }
            // get home page
            var homePage = pageService.GetPageBySlug(slug);
            if (homePage == null)
            {
                var post = postService.GetPostBySlug(slug);
                if (post == null)
                {
                    return View("Page404");
                }
                else
                {
                    PostViewModel postVM = new PostViewModel();
                    postVM.Id = post.Id;
                    postVM.Title = post.Title;
                    postVM.Slug = post.Slug;
                    postVM.Body = post.Body;
                    postVM.Description = post.Description;
                    postVM.IsPublished = post.IsPublished;
                    postVM.AddedDate = post.AddedDate;
                    postVM.SeoTitle = post.SeoTitle;
                    postVM.SeoDescription = post.SeoDescription;
                    postVM.SeoKeywords = post.SeoKeywords;
                    postVM.Photo = post.Photo;
                    postVM.ViewCount = post.ViewCount;


                    return View("Post", postVM);
                }
            }
            else
            {
                PageViewModel pageVM = new PageViewModel();
                pageVM.Id = homePage.Id;
                pageVM.Title = homePage.Title;
                pageVM.Slug = homePage.Slug;
                pageVM.Body = homePage.Body;
                pageVM.Template = homePage.Template;
                pageVM.SeoTitle = homePage.SeoTitle;
                pageVM.SeoKeywords = homePage.SeoKeywords;
                pageVM.SeoDescription = homePage.SeoDescription;
                if (homePage.TemplateId != null)
                {
                    return View(homePage.Template.ViewName, pageVM);
                }
                return View(pageVM);
            }
        }

        public IEnumerable<Post> Posts()
        {
            return postService.GetPosts();
        }
        public IActionResult Page404()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
