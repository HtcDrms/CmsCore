using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Data.Repositories
{
    public class GalleryItemRepository:RepositoryBase<GalleryItem>,IGalleryItemRepository
    {
        public GalleryItemRepository(ApplicationDbContext dbContext)
                : base(dbContext) { }
        public IEnumerable<GalleryItem> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');

            var query = this.DbContext.GalleryItems.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(p => p.Id.ToString().Contains(sSearch) || p.Title.Contains(sSearch));
                }
            }

            var allGalleries = query;
            IEnumerable<GalleryItem> filteredGalleries = allGalleries;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredGalleries = filteredGalleries.OrderBy(p => p.Id);
                        break;
                    case 2:
                        filteredGalleries = filteredGalleries.OrderBy(p => p.Title);
                        break;
                    case 3:
                        filteredGalleries = filteredGalleries.OrderBy(p => p.AddedBy);
                        break;
                    case 4:
                        filteredGalleries = filteredGalleries.OrderBy(p => p.AddedDate);
                        break;

                    default:
                        filteredGalleries = filteredGalleries.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredGalleries = filteredGalleries.OrderByDescending(p => p.Id);
                        break;
                    case 2:
                        filteredGalleries = filteredGalleries.OrderByDescending(p => p.Title);
                        break;
                    case 3:
                        filteredGalleries = filteredGalleries.OrderByDescending(p => p.AddedBy);
                        break;
                    case 4:
                        filteredGalleries = filteredGalleries.OrderByDescending(p => p.AddedDate);
                        break;

                    default:
                        filteredGalleries = filteredGalleries.OrderByDescending(c => c.Id);
                        break;
                }
            }
            var displayedGalleries = filteredGalleries.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedGalleries = displayedGalleries.Take(displayLength);
            }
            totalRecords = allGalleries.Count();
            totalDisplayRecords = filteredGalleries.Count();
            return displayedGalleries.ToList();
        }
    }
    public interface IGalleryItemRepository : IRepository<GalleryItem>
    {
        IEnumerable<GalleryItem> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}
