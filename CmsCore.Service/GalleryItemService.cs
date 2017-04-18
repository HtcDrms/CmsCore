using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Service
{
    public interface IGalleryItemService
    {
        IEnumerable<GalleryItem> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<GalleryItem> GetGalleryItems();
        GalleryItem GetGalleryItem(long id);
        int CountGalleryItem();
        void UpdateCategories(long galItemId, string SelectedCategories);
        void CreateGalleryItem(GalleryItem galleryItem);
        void UpdateGalleryItem(GalleryItem galleryItem);
        void DeleteGalleryItem(long id);
        void SaveGalleryItem();
    }
    public class GalleryItemService : IGalleryItemService
    {
        private readonly IGalleryItemRepository galleryItemRepository;
        private readonly IUnitOfWork unitOfWork;
        public GalleryItemService(IGalleryItemRepository galleryItemRepository, IUnitOfWork unitOfWork)
        {
            this.galleryItemRepository = galleryItemRepository;
            this.unitOfWork = unitOfWork;
        }
        #region IGalleryItemService Members
        public IEnumerable<GalleryItem> GetGalleryItems()
        {
            var galleryItems = galleryItemRepository.GetAll();
            return galleryItems;
        }

        public void UpdateCategories(long galItemId, string SelectedCategories)
        {
            galleryItemRepository.UpdateGalleryItemGalleryItemCategories(galItemId, SelectedCategories);
        }

        public GalleryItem GetGalleryItem(long id)
        {
            var galleryItem = galleryItemRepository.GetById(id,"GalleryItemGalleryItemCategories");
            return galleryItem;
        }
        public void CreateGalleryItem(GalleryItem galleryItem)
        {
            galleryItemRepository.Add(galleryItem);
        }
        public void UpdateGalleryItem(GalleryItem galleryItem)
        {
            galleryItemRepository.Update(galleryItem);
        }
        public void DeleteGalleryItem(long id)
        {
            galleryItemRepository.Delete(x => x.Id == id);
        }
        public void SaveGalleryItem()
        {
            unitOfWork.Commit();
        }
        public int CountGalleryItem()
        {
            return galleryItemRepository.GetAll().Count();
        }
        public IEnumerable<GalleryItem> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var Galleries = galleryItemRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return Galleries;
        }
        #endregion
    }
}
