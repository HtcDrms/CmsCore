using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{
    public interface IPostService
    {
        IEnumerable<Post> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostsByCategoryNames(string categoryNames, int count);
        IEnumerable<Post> GetPostsByCategoryNames(string categoryNames, int count,long id);
        Post GetPost(long id);
        string GetCategoryName(long id);
        void UpdatePostPostCategories(long postId, string SelectedCategories);
        IEnumerable<Post> PopulerPost(int total,long id);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(long id);
        Post GetPostBySlug(string Slug);
        void SavePost();
    }
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }
        #region IPostService Members
        public string GetCategoryName(long id)
        {
            return postRepository.GetCategoryName(id);
        }
        public IEnumerable<Post> GetPostsByCategoryNames(string categoryNames, int count)
        {
            string[] categories;
            if (categoryNames == "")
            {
                categories = new string[0];
            }
            else
            {
                categories = categoryNames.Split(',');
            }
            
            for (var i = 0; i < categories.Length; i++)
            {
                categories[i] = categories[i].Trim().ToLower();
            }
            var posts = postRepository.GetPostsByCategoryNames(categories, count);
            return posts;
        }
        public IEnumerable<Post> GetPostsByCategoryNames(string categoryNames, int count,long id)
        {
            string[] categories;
            if (categoryNames == "")
            {
                categories = new string[0];
            }
            else
            {
                categories = categoryNames.Split(',');
            }

            for (var i = 0; i < categories.Length; i++)
            {
                categories[i] = categories[i].Trim().ToLower();
            }
            var posts = postRepository.GetPostsByCategoryNames(categories, count,id);
            return posts;
        }

        public IEnumerable<Post> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var posts = postRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return posts;
        }
        public IEnumerable<Post> GetPosts()
        {
            var posts = postRepository.GetAll().OrderByDescending(p => p.AddedDate);
            return posts;
        }

        public Post GetPost(long id)
        {
            var post = postRepository.GetById(id,"PostPostCategories");
            return post;
        }
        public IEnumerable<Post> PopulerPost(int total,long id)
        {
            var post = postRepository.GetAll().Where(u=>u.Id!=id).OrderByDescending(m => m.ViewCount).Take(total).ToList();
            return post;
        }

        public void CreatePost(Post post)
        {
            postRepository.Add(post);
        }
        public void UpdatePost(Post post)
        {
            postRepository.Update(post);
        }
        public void DeletePost(long id)
        {
            postRepository.Delete(p => p.Id == id);
        }

        public Post GetPostBySlug(string Slug)
        {
            return postRepository.GetPostBySlug(Slug);
        }

        public void UpdatePostPostCategories(long postId, string SelectedCategories)
        {
            postRepository.UpdatePostPostCategories(postId, SelectedCategories);
        }

        public void SavePost()
        {
            unitOfWork.Commit();
        }


        #endregion
    }
}
