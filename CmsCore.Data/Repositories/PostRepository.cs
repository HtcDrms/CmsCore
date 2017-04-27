using CmsCore.Data.Infrastructure;
using CmsCore.Model.Dtos;
using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {

        public PostRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }


        public List<SearchDto> Search(string words)
        {
            words = words.Trim();
            var searchWords = words.Split(' ');
            var query = DbContext.Posts.AsQueryable();
            List<SearchDto> dto = new List<SearchDto>();
            foreach (string word in searchWords)
            {
                if (word != null && word != "")
                {
                  dto=query.Where(w=>w.IsPublished==true).Where(p => p.Body.Contains(word) || p.Title.Contains(word) || p.Description.Contains(word) || p.SeoDescription.Contains(word) || p.SeoKeywords.Contains(word) || p.SeoKeywords.Contains(word)).Select(s=>new SearchDto { Title=s.Title, Slug=s.Slug,Description=s.Description,ViewCount=s.ViewCount}).ToList();
                }
            }
            return dto;
        }
        public string GetCategoryName(long id)
        {
            var postcat=DbContext.PostCategories.ToList();
            var pcId = DbContext.PostPostCategories.Where(w => w.PostId == id).FirstOrDefault().PostCategoryId;
            var val = postcat.FirstOrDefault(p => p.Id == pcId).Name;
            return val;
        }

        public IEnumerable<Post> GetPostsByCategoryNames(string[] categories, int count )
        {
            if (categories.Length > 0)
            {
                return (from pc in DbContext.PostPostCategories join p in DbContext.Posts on pc.PostId equals p.Id join c in DbContext.PostCategories on pc.PostCategoryId equals c.Id where (categories.Length > 0 ? categories.Contains(c.Name.ToLower()) : true) orderby p.AddedDate descending select p).Take(count).ToList();
            }
            else
            {
                return (from p in DbContext.Posts orderby p.AddedDate descending select p).Take(count).ToList();
            }
        }
        public IEnumerable<Post> GetPostsByCategoryNames(string[] categories, int count, long? id)
        {
            if (categories.Length > 0)
            {
                return (from pc in DbContext.PostPostCategories join p in DbContext.Posts on pc.PostId equals p.Id join c in DbContext.PostCategories on pc.PostCategoryId equals c.Id where (categories.Length > 0 ? categories.Contains(c.Name.ToLower()) : true) orderby p.AddedDate descending select p).Where(c => c.Id != id).Take(count).ToList();
            }
            else
            {
                return (from p in DbContext.Posts orderby p.AddedDate descending select p).Where(c => c.Id != id).Take(count).ToList();
            }

        }

        public IEnumerable<Post> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');
            var query = DbContext.Posts.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(p => p.Id.ToString().Contains(sSearch) || p.Slug.Contains(sSearch) || p.Title.Contains(sSearch) || p.Body.Contains(sSearch) || p.AddedDate.ToString().Contains(sSearch) || p.AddedBy.Contains(sSearch) || p.ModifiedDate.ToString().Contains(sSearch) || p.ModifiedBy.Contains(sSearch) || p.ViewCount.ToString().Contains(sSearch));
                }
            }
            var allPosts = query;
            IEnumerable<Post> filteredPosts = allPosts;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredPosts = filteredPosts.OrderBy(p => p.Id);
                        break;
                    case 2:
                        filteredPosts = filteredPosts.OrderBy(p => p.Slug);
                        break;
                    case 3:
                        filteredPosts = filteredPosts.OrderBy(p => p.Title);
                        break;
                    case 4:
                        filteredPosts = filteredPosts.OrderBy(p => p.AddedDate);
                        break;
                    case 5:
                        filteredPosts = filteredPosts.OrderBy(p => p.ViewCount);
                        break;
                    default:
                        filteredPosts = filteredPosts.OrderBy(p => p.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredPosts = filteredPosts.OrderByDescending(p => p.Id);
                        break;
                    case 2:
                        filteredPosts = filteredPosts.OrderByDescending(p => p.Slug);
                        break;
                    case 3:
                        filteredPosts = filteredPosts.OrderByDescending(p => p.Title);
                        break;
                    case 4:
                        filteredPosts = filteredPosts.OrderByDescending(p => p.AddedDate);
                        break;
                    case 5:
                        filteredPosts = filteredPosts.OrderBy(p => p.ViewCount);
                        break;
                    default:
                        filteredPosts = filteredPosts.OrderByDescending(p => p.Id);
                        break;
                }

            }
            var displayedPosts = filteredPosts.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedPosts = displayedPosts.Take(displayLength);
            }
            totalRecords = allPosts.Count();
            totalDisplayRecords = filteredPosts.Count();
            return displayedPosts.ToList();
        }

        //public override void Update(Post entity)
        //{
        //    var post = DbContext.Posts.Include("PostPostCategories").Where(c => c.Id == entity.Id).Single();

        //    post.PostPostCategories.Clear();

        //    foreach (var category in entity.PostPostCategories)
        //    {
        //        post.PostPostCategories.Add(category);
        //    }

        //    post.Body = entity.Body;
        //    post.AddedDate = entity.AddedDate;
        //    post.AddedBy = entity.AddedBy;
        //    post.Id = entity.Id;
        //    post.Slug = entity.Slug;
        //    post.Title = entity.Title;
        //    post.ModifiedDate = entity.ModifiedDate;
        //    post.ModifiedBy = entity.ModifiedBy;
        //    post.SeoDescription = entity.SeoDescription;
        //    post.SeoKeywords = entity.SeoKeywords;
        //    post.SeoTitle = entity.SeoTitle;
        //    DbContext.SaveChanges();
        //}
        public Post GetPostBySlug(string slug)
        {
            var post = DbContext.Posts.Where(p => p.Slug == slug).FirstOrDefault();
            return post;
        }
        public void UpdatePostPostCategories(long postId, string SelectedCategories)
        {
            Post post = DbContext.Posts.Find(postId);
            
            if (SelectedCategories != null)
            {
                post.PostPostCategories.Clear();
                DbContext.SaveChanges();
                var cateArray = SelectedCategories.Split(',');

                foreach (var item in cateArray)
                {
                    post.PostPostCategories.Add(new PostPostCategory { PostId = post.Id, PostCategoryId = Convert.ToInt64(item) });
                }
            }
            DbContext.Update(post);
            DbContext.SaveChanges();
        }
    }
    public interface IPostRepository : IRepository<Post>
    {
        Post GetPostBySlug(string slug);
        void UpdatePostPostCategories(long postId, string SelectedCategories);
        string GetCategoryName(long id);
        List<SearchDto> Search(string words);
        IEnumerable<Post> GetPostsByCategoryNames(string[] categories, int count);
        IEnumerable<Post> GetPostsByCategoryNames(string[] categories, int count,long? id);
        IEnumerable<Post> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}
