using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Service
{
    public interface IGalleryItemCategoryService
    {
        IEnumerable<GalleryItemCategory> GetParentGalleryItemCategories();
        IEnumerable<GalleryItemCategory> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<GalleryItemCategory> GetGalleryItemCategories();
        IEnumerable<GalleryItemCategory> GetParentCategories();
        IEnumerable<GalleryItemCategory> GetGalleryItemCategoriesById(long id);
        GalleryItemCategory GetGalleryItemCategory(long id);
        void CreateGalleryItemCategory(GalleryItemCategory galleryItemCategory);
        void UpdateGalleryItemCategory(GalleryItemCategory galleryItemCategory);
        void DeleteGalleryItemCategory(long id);
        void SaveGalleryItemCategory();
    }
    public class GalleryItemCategoryService : IGalleryItemCategoryService
    {
        private readonly IGalleryItemCategoryRepository galleryItemCategoryRepository;
        private readonly IUnitOfWork unitOfWork;
        public GalleryItemCategoryService(IGalleryItemCategoryRepository galleryItemCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.galleryItemCategoryRepository = galleryItemCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IGalleryItemCategoryService Members
        public IEnumerable<GalleryItemCategory> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var galleryItemCategories = galleryItemCategoryRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return galleryItemCategories;
        }
        
        public IEnumerable<GalleryItemCategory> GetGalleryItemCategories()
        {
            var galleryItemCategories = galleryItemCategoryRepository.GetAll("ChildCategories");
            return galleryItemCategories;

        }
        public IEnumerable<GalleryItemCategory> GetParentGalleryItemCategories()
        {
            var galleryItemCategories = galleryItemCategoryRepository.GetMany(c => c.ParentCategoryId == null, "ChildCategories");
            return galleryItemCategories;
        }
        public IEnumerable<GalleryItemCategory> GetGalleryItemCategoriesById(long id)
        {
            var galleryItemCategories = galleryItemCategoryRepository.GetMany(c => c.Id == id, "ChildCategories");
            return galleryItemCategories;
        }
        public GalleryItemCategory GetGalleryItemCategory(long id)
        {
            var galleryItemCategory = galleryItemCategoryRepository.GetById(id);
            return galleryItemCategory;
        }
        public void CreateGalleryItemCategory(GalleryItemCategory galleryItemCategory)
        {
            galleryItemCategoryRepository.Add(galleryItemCategory);
        }
        public void UpdateGalleryItemCategory(GalleryItemCategory galleryItemCategory)
        {
            galleryItemCategoryRepository.Update(galleryItemCategory);
        }
        public void DeleteGalleryItemCategory(long id)
        {
            galleryItemCategoryRepository.Delete(pc => pc.Id == id);
        }
        public void SaveGalleryItemCategory()
        {
            unitOfWork.Commit();
        }
        public IEnumerable<GalleryItemCategory> GetParentCategories()
        {
            var galleryItemCategory = galleryItemCategoryRepository.GetMany(s => s.ParentCategoryId == null);
            return galleryItemCategory;
        }

        #endregion
    }
}
