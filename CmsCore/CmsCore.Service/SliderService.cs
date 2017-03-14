using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{
    public interface ISliderService
    {
        IEnumerable<Slider> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Slider> GetSliders();
        Slider GetSlider(long id);
        List<Slide> GetSlideBySliderId(long id);
        void CreateSlider(Slider slider);
        void UpdateSlider(Slider slider);
        void DeleteSlider(long id);
        void SaveSlider();

    }
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository sliderRepository;
        private readonly IUnitOfWork unitOfWork;

        public SliderService(ISliderRepository sliderRepository, IUnitOfWork unitOfWork)
        {
            this.sliderRepository = sliderRepository;
            this.unitOfWork = unitOfWork;
        }

        #region ISliderService Members

        public IEnumerable<Slider> GetSliders()
        {
            var sliders = sliderRepository.GetAll();
            return sliders;
        }
        public List<Slide> GetSlideBySliderId(long id)
        {
            Slider slider = sliderRepository.GetById(id,"Slides");
            return slider.Slides.OrderBy(c => c.Position).ToList();
        }
        public Slider GetSlider(long id)
        {
            var slider = sliderRepository.GetById(id);
            return slider;
        }
        public void CreateSlider(Slider slider)
        {
            sliderRepository.Add(slider);
        }
        public void UpdateSlider(Slider slider)
        {
            sliderRepository.Update(slider);
        }
        public void DeleteSlider(long id)
        {
            sliderRepository.Delete(s => s.Id == id);
        }
        public void SaveSlider()
        {
            unitOfWork.Commit();
        }
        public IEnumerable<Slider> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var sliders = sliderRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return sliders;
        }

        #endregion

    }
}
