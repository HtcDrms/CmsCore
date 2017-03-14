using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{

    public interface IFeedbackService
    {
        IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Feedback> GetFeedbacks();
        List<FeedbackValue> GetFeedbackValueByFeedbackId(long id);
        List<Feedback> GetFeedbacks(string User, int id);
        Feedback GetFeedback(int id);
        void CreateFeedback(Feedback Feedback);
        void UpdateFeedback(Feedback Feedback);
        void DeleteFeedback(long id);
        int CountFeedback();
        void SaveFeedback();

    }
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IUnitOfWork unitOfWork;
        public FeedbackService(IFeedbackRepository FeedbackRepository, IUnitOfWork unitOfWork)
        {
            this.feedbackRepository = FeedbackRepository;
            this.unitOfWork = unitOfWork;
        }
        #region IFeedbackService Members
        public IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var Feedbacks = feedbackRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);

            return Feedbacks;
        }
        public IEnumerable<Feedback> GetFeedbacks()
        {
            var Feedbacks = feedbackRepository.GetAll("Form");
            return Feedbacks;
        }
        public Feedback GetFeedback(int id)
        {
            var Feedback = feedbackRepository.GetById(id);
            return Feedback;
        }
        public List<FeedbackValue> GetFeedbackValueByFeedbackId(long id)
        {
            Feedback feedback = feedbackRepository.GetById(id, "FeedbackValues");
            return feedback.FeedbackValues.OrderBy(c => c.Position).ToList();
        }
        public void CreateFeedback(Feedback Feedback)
        {
            feedbackRepository.Add(Feedback);
        }
        public void UpdateFeedback(Feedback Feedback)
        {
            feedbackRepository.Update(Feedback);
        }
        public void DeleteFeedback(long id)
        {
            feedbackRepository.Delete(f => f.Id == id);
        }
        public void SaveFeedback()
        {
            unitOfWork.Commit();
        }
        public int CountFeedback()
        {
            return feedbackRepository.GetAll().Count();
        }

        public List<Feedback> GetFeedbacks(string User, int id)
        {
            return feedbackRepository.GetMany(c => c.UserName == User && c.FormId == id).ToList();
        }
        #endregion
    }

}
