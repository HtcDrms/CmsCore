using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data.Repositories
{
    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(ApplicationDbContext dbContext)
                : base(dbContext) { }
        public IEnumerable<Slide> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');

            var query = this.DbContext.Slides.AsQueryable();
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
                    query = query.Where(p => p.Id.ToString().Contains(sSearch) || p.Title.Contains(sSearch) || p.SubTitle.Contains(sSearch)||p.AddedBy.Contains(sSearch)|| (dateParsed == true ? p.AddedDate == dDate : false));
                }
            }

            var allSlides = query;
            IEnumerable<Slide> filteredSlides = allSlides;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredSlides = filteredSlides.OrderBy(p => p.Id);
                        break;
                    case 2:
                        filteredSlides = filteredSlides.OrderBy(p => p.Title);
                        break;
                    case 3:
                        filteredSlides = filteredSlides.OrderBy(p => p.SubTitle);
                        break;
                    case 4:
                        filteredSlides = filteredSlides.OrderBy(p => p.AddedBy);
                        break;
                    case 5:
                        filteredSlides = filteredSlides.OrderBy(p => p.AddedDate);
                        break;

                    default:
                        filteredSlides = filteredSlides.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredSlides = filteredSlides.OrderBy(p => p.Id);
                        break;
                    case 2:
                        filteredSlides = filteredSlides.OrderBy(p => p.Title);
                        break;
                    case 3:
                        filteredSlides = filteredSlides.OrderBy(p => p.SubTitle);
                        break;
                    case 4:
                        filteredSlides = filteredSlides.OrderBy(p => p.AddedBy);
                        break;
                    case 5:
                        filteredSlides = filteredSlides.OrderBy(p => p.AddedDate);
                        break;

                    default:
                        filteredSlides = filteredSlides.OrderBy(c => c.Id);
                        break;
                }
            }
            var displayedSlides = filteredSlides.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedSlides = displayedSlides.Take(displayLength);
            }
            totalRecords = allSlides.Count();
            totalDisplayRecords = filteredSlides.Count();
            return displayedSlides.ToList();
        }

    }
    public interface ISlideRepository : IRepository<Slide>
    {
        IEnumerable<Slide> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}