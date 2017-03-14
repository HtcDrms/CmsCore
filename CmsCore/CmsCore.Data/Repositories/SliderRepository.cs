using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data.Repositories
{
    public class SliderRepository : RepositoryBase<Slider>, ISliderRepository
    {
        public SliderRepository(ApplicationDbContext dbContext)
                : base(dbContext) { }
        public IEnumerable<Slider> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');

            var query = this.DbContext.Sliders.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(p => p.Id.ToString().Contains(sSearch) || p.IsPublished.ToString().Contains(sSearch));
                }
            }

            var allSliders = query;
            IEnumerable<Slider> filteredSliders = allSliders;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredSliders = filteredSliders.OrderBy(p => p.Id);
                        break;
                    case 2:
                        filteredSliders = filteredSliders.OrderBy(p => p.IsPublished);
                        break;
                    case 3:
                        filteredSliders = filteredSliders.OrderBy(p => p.AddedBy);
                        break;
                    case 4:
                        filteredSliders = filteredSliders.OrderBy(p => p.AddedDate);
                        break;

                    default:
                        filteredSliders = filteredSliders.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredSliders = filteredSliders.OrderByDescending(p => p.Id);
                        break;
                    case 2:
                        filteredSliders = filteredSliders.OrderByDescending(p => p.IsPublished);
                        break;
                    case 3:
                        filteredSliders = filteredSliders.OrderByDescending(p => p.AddedBy);
                        break;
                    case 4:
                        filteredSliders = filteredSliders.OrderByDescending(p => p.AddedDate);
                        break;

                    default:
                        filteredSliders = filteredSliders.OrderByDescending(c => c.Id);
                        break;
                }
            }
            var displayedSliders = filteredSliders.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedSliders = displayedSliders.Take(displayLength);
            }
            totalRecords = allSliders.Count();
            totalDisplayRecords = filteredSliders.Count();
            return displayedSliders.ToList();
        }

    }
    public interface ISliderRepository : IRepository<Slider>
    {
        IEnumerable<Slider> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}
