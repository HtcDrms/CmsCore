using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{
    public interface ISlideService
    {
        IEnumerable<Slide> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        void CreateSlide(Slide slide);
        void UpdateSlide(Slide slide);
        void DeleteSlide(long id);
        int CountSlide();
        IEnumerable<Slide> GetSlides();
        Slide GetSlide(long id);
        void SaveSlide();

    }
    public class SlideService:ISlideService
    {
        private readonly ISlideRepository slideRepository;
        private readonly IUnitOfWork unitOfWork;

        public SlideService (ISlideRepository slideRepository,IUnitOfWork unitOfWork)
        {
            this.slideRepository = slideRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISlideService Members

        public IEnumerable<Slide> GetSlides()
        {
            var slides = slideRepository.GetAll();
            return slides;
        }
        public Slide GetSlide(long id)
        {
            var slide = slideRepository.GetById(id);
            return slide;
        }

        public void CreateSlide(Slide slide)
        {
            slideRepository.Add(slide);
        }
        public void UpdateSlide(Slide slide)
        {
            slideRepository.Update(slide);
        }
        public void DeleteSlide(long id)
        {
            slideRepository.Delete(s => s.Id == id);
        }
        public void SaveSlide()
        {
            unitOfWork.Commit();
        }
        public int CountSlide()
        {
            return slideRepository.GetAll().Count();
        }
        public IEnumerable<Slide> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var slides = slideRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return slides;
        }

        #endregion

    }
}
