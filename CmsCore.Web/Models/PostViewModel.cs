using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.Models
{
    public class PostViewModel:BaseEntity
    {
        public PostViewModel()
        {
            IsPublished = true;
            ViewCount = 0;
            PostPostCategories = new HashSet<PostPostCategory>();
        }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string CategoryName { get; set; }
        public long ViewCount { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public bool IsPublished { get; set; }

        public virtual ICollection<PostPostCategory> PostPostCategories { get; set; }
        public long[] PostCategoryId { get; set; }
    }
}
