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
        Post GetPost(long id);

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
            var post = postRepository.GetById(id);
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


        public void SavePost()
        {
            unitOfWork.Commit();
        }


        #endregion
    }
}
