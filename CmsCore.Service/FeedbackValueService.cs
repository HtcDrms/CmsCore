using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{
    public interface IFeedbackValueService
    {

        void CreateFeedbackValue(FeedbackValue feedbackValue);
        void UpdateFeedbackValue(FeedbackValue feedbackValue);
        void DeleteFeedbackValue(long id);
        void SaveFeedbackValue();
        int CountFeedbackValue();
    }
    public class FeedbackValueService:IFeedbackValueService
    {
        private readonly IFeedbackValueRepository feedbackValueRepository;
        private readonly IUnitOfWork unitOfWork;
        public FeedbackValueService(IFeedbackValueRepository feedbackValueRepository, IUnitOfWork unitOfWork)
        {
            this.feedbackValueRepository = feedbackValueRepository;
            this.unitOfWork = unitOfWork;
        }

        
        public void CreateFeedbackValue(FeedbackValue feedbackValue)
        {
            feedbackValueRepository.Add(feedbackValue);
        }
        public void UpdateFeedbackValue(FeedbackValue feedbackValue)
        {
            feedbackValueRepository.Update(feedbackValue);
        }
        public void DeleteFeedbackValue(long id)
        {            
            feedbackValueRepository.Delete(f => f.Id == id);
        }
        public void SaveFeedbackValue()
        {
            unitOfWork.Commit();
        }
        public int CountFeedbackValue()
        {
            return feedbackValueRepository.GetAll().Count();
        }
    }
}
