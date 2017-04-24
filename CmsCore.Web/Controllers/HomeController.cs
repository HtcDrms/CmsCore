using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Web.Models;
using CmsCore.Model.Entities;
using Sakura.AspNetCore.Mvc;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace CmsCore.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPageService pageService;
        private readonly IPostService postService;
        private readonly IFeedbackService feedbackService;

        public HomeController(IPageService pageService, IPostService postService, IFeedbackService feedbackService)
        {
            this.pageService = pageService;
            this.postService = postService;
            this.feedbackService = feedbackService;
        }

        [HttpPost]
        public IActionResult PostForm(IFormCollection formCollection)
        {
            string body = "";
            feedbackService.FeedbackPost(formCollection, null);
            //foreach (var item in feedbackvalue)
            //{
            //    if (item.FieldType.ToString() == "multipleChoice" || item.FieldType.ToString() == "singleChoice" || item.FieldType.ToString() == "radioButtons")
            //    {
            //        TagBuilder text = new TagBuilder("text");
            //        text.InnerHtml.SetHtmlContent(item.FormFieldName);
            //        TagBuilder list = new TagBuilder("ul");
            //        var items = item.Value.Split(',');
            //        string element = "";
            //        foreach (var item2 in items)
            //        {
            //            TagBuilder singlechoice = new TagBuilder("li");
            //            singlechoice.Attributes.Add("value", item2);
            //            singlechoice.InnerHtml.SetHtmlContent(item2);
            //            element += singlechoice.ToString() + "<br/>";
            //        }
            //        list.InnerHtml.SetHtmlContent(element);
            //        var write = new System.IO.StringWriter();
            //        text.WriteTo(write, HtmlEncoder.Default);
            //        var write2 = new System.IO.StringWriter();
            //        list.WriteTo(write2, HtmlEncoder.Default);
            //        body = body + text.ToString() + "<br/>" + write2.ToString() + "<br/>";

            //    }
            //    else
            //    {
            //        TagBuilder text = new TagBuilder("text");
            //        text.InnerHtml.SetHtmlContent(item.FormFieldName);
            //        TagBuilder text2 = new TagBuilder("text");
            //        text2.InnerHtml.SetHtmlContent(item.Value);
            //        var write = new System.IO.StringWriter();
            //        text.WriteTo(write, HtmlEncoder.Default);
            //        var write2 = new System.IO.StringWriter();
            //        text2.WriteTo(write2, HtmlEncoder.Default);
            //        body = body + write.ToString() + ":" + write2.ToString() + "<br/>";

            //    }
            //}
            //feedbackService.FeedbackPostMail(body, Convert.ToInt64(formCollection["FormId"]));
            return RedirectToAction("Index");
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
                    postVM.CategoryName = postService.GetCategoryName(post.Id);
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
