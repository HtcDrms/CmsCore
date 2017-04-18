using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Model.Entities;
using CmsCore.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsCore.Admin.Controllers
{
    [Authorize(Roles = "ADMIN,POST")]
    public class PostController : BaseController
    {
        private readonly IPostService postService;
        private readonly IPostCategoryService postCategoryService;
        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this.postService = postService;
            this.postCategoryService = postCategoryService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = postService.GetPosts();
            return View(posts);
        }

        public IActionResult Create()
        {
            
            var postVM = new PostViewModel();
            ViewBag.CategoryList = postCategoryService.GetPostCategories();
            return View(postVM);
        }
        [HttpPost]
        public IActionResult Create(PostViewModel postVM)
        {

            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.Title = postVM.Title;
                post.Slug = postVM.Slug;
                post.Body = postVM.Body;
                post.Description = postVM.Description;
                post.Photo = postVM.Photo;
                post.Meta1 = postVM.Meta1;
                post.Meta2 = postVM.Meta2;
                post.IsPublished = postVM.IsPublished;

                post.SeoTitle = postVM.SeoTitle;
                post.SeoKeywords = postVM.SeoKeywords;
                post.SeoDescription = postVM.SeoDescription;
                
                postService.CreatePost(post);
                postService.SavePost();

                postService.UpdatePostPostCategories(post.Id, postVM.categoriesHidden);
                
                return RedirectToAction("Index", "Post");
            }
            ViewBag.CategoryList = postCategoryService.GetPostCategories();
            return View(postVM);
        }

        public IActionResult Edit(long id)
        {
            var post = postService.GetPost(id);
            ViewBag.CategoryList = postCategoryService.GetPostCategories();
            ViewBag.CheckList = post.PostPostCategories;

            PostViewModel postVM = new PostViewModel();
            postVM.Id = post.Id;
            postVM.Title = post.Title;
            postVM.Slug = post.Slug;
            postVM.Body = post.Body;
            postVM.Description = post.Description;
            postVM.Photo = post.Photo;
            postVM.Meta1 = post.Meta1;
            postVM.Meta2 = post.Meta2;
            postVM.IsPublished = post.IsPublished;
            postVM.PostPostCategories = post.PostPostCategories;
            postVM.ModifiedDate = post.ModifiedDate;
            postVM.ModifiedBy = post.ModifiedBy;
            postVM.AddedBy = post.AddedBy;
            postVM.AddedDate = post.AddedDate;
            postVM.SeoTitle = post.SeoTitle;
            postVM.SeoDescription = post.SeoDescription;
            postVM.SeoKeywords = post.SeoKeywords;
            
            return View(postVM);
        }

        [HttpPost]
        public IActionResult Edit(PostViewModel postVM)
        {
            if (ModelState.IsValid)
            {
                Post post = postService.GetPost(postVM.Id);
                
                post.Title = postVM.Title;
                post.Slug = postVM.Slug;
                post.Body = postVM.Body;
                post.Description = postVM.Description;
                post.Photo = postVM.Photo;
                post.Meta1 = postVM.Meta1;
                post.Meta2 = postVM.Meta2;
                post.IsPublished = postVM.IsPublished;
                
                post.SeoTitle = postVM.SeoTitle;
                post.SeoDescription = postVM.SeoDescription;
                post.SeoKeywords = postVM.SeoKeywords;
                postService.UpdatePost(post);
                postService.SavePost();

                postService.UpdatePostPostCategories(post.Id, postVM.categoriesHidden);

                return RedirectToAction("Index", "Post");
            }
            var postR = postService.GetPost(postVM.Id);
            ViewBag.CategoryList = postCategoryService.GetPostCategories();
            ViewBag.CheckList = postR.PostPostCategories;

            return View(postVM);
        }

        public IActionResult Delete(long id)
        {
            postService.DeletePost(id);
            postService.SavePost();
            return RedirectToAction("Index", "Post");
        }

        public IActionResult Details(int id)
        {
            var post = postService.GetPost(id);
            return View(post);
        }
        public IActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPosts = postService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);

            var result = from p in displayedPosts
                         select new[] { p.Id.ToString(), p.Title.ToString(), p.AddedBy.ToString(), p.ViewCount.ToString(), p.ModifiedDate.ToString(), string.Empty };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = result.ToList()
            });
        }
    }
}
