using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{
    public interface IGalleryService
    {
        IEnumerable<Gallery> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Gallery> GetGalleries();
        IEnumerable<GalleryItem> GetGalleryItems(string galleryName, int count);
        Gallery GetGallery(long id);
        void CreateGallery(Gallery slider);
        void UpdateGallery(Gallery slider);
        void DeleteGallery(long id);
        void SaveGallery();

    }
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository galleryRepository;
        private readonly IUnitOfWork unitOfWork;

        public GalleryService(IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
            this.galleryRepository = galleryRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISliderService Members

        public IEnumerable<Gallery> GetGalleries()
        {
            var galleries = galleryRepository.GetAll();
            return galleries;
        }
        public Gallery GetGallery(long id)
        {
            var gallery = galleryRepository.GetById(id);
            return gallery;
        }
        public IEnumerable<GalleryItem> GetGalleryItems(string galleryName, int count)
        {
            galleryName = galleryName.ToLower();
            var galleryItems = galleryRepository.Get(g => g.Name.ToLower() == galleryName && g.IsPublished == true, "GalleryItems").GalleryItems.Where(gi => gi.IsPublished == true).Take(count).ToList();
            return galleryItems;
        }
        public void CreateGallery(Gallery gallery)
        {
            galleryRepository.Add(gallery);
        }
        public void UpdateGallery(Gallery gallery)
        {
            galleryRepository.Update(gallery);
        }
        public void DeleteGallery(long id)
        {
            galleryRepository.Delete(s => s.Id == id);
        }
        public void SaveGallery()
        {
            unitOfWork.Commit();
        }
        public IEnumerable<Gallery> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var galleries = galleryRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return galleries;
        }

        #endregion

    }
}
