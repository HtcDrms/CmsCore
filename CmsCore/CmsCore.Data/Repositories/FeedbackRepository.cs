using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data.Repositories
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext dbFactory)
            : base(dbFactory) { }
        public IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');
            
            var query = this.DbContext.Feedbacks.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    DateTime dDate;
                    bool dateParsed = false;
                    if (DateTime.TryParse(sSearch, out dDate))
                    {
                        dDate = DateTime.Parse(sSearch);
                        dateParsed = true;
                    }
                    query = query.Where(c => c.Id.ToString().Contains(sSearch) || c.FormName.Contains(sSearch) || c.UserName.Contains(sSearch) || c.IP.Contains(sSearch) || (dateParsed == true ? c.AddedDate == dDate : false) || (dateParsed == true ? c.SentDate == dDate : false));
                }
            }
            var allFeedbacks = query;

            IEnumerable<Feedback> filteredFeedbacks = allFeedbacks;

            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 0:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.Id);
                        break;
                    case 1:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.FormName);
                        break;
                    case 2:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.UserName);
                        break;
                    case 3:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.IP);
                        break;
                    case 4:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.AddedDate);
                        break;
                    case 5:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.SentDate);
                        break;
                    default:
                        filteredFeedbacks = filteredFeedbacks.OrderBy(c => c.UserName);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {

                    case 0:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.Id);
                        break;
                    case 1:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.FormName);
                        break;
                    case 2:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.UserName);
                        break;
                    case 3:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.IP);
                        break;
                    case 4:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.AddedDate);
                        break;
                    case 5:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.SentDate);
                        break;
                    default:
                        filteredFeedbacks = filteredFeedbacks.OrderByDescending(c => c.UserName);
                        break;
                }
            }

            var displayedFeedbacks = filteredFeedbacks.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedFeedbacks = displayedFeedbacks.Take(displayLength);
            }
            totalRecords = allFeedbacks.Count();
            totalDisplayRecords = filteredFeedbacks.Count();
            return displayedFeedbacks.ToList();
        }
    }
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}
