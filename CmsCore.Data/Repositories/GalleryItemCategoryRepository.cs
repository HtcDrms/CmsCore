using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Data.Repositories
{
    public class GalleryItemCategoryRepository : RepositoryBase<GalleryItemCategory>, IGalleryItemCategoryRepository
    {
        public GalleryItemCategoryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        { }

        public IEnumerable<GalleryItemCategory> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');
            var query = this.DbContext.GalleryItemCategories.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(c => c.Id.ToString().Contains(sSearch) || c.Name.Contains(sSearch) || c.Description.Contains(sSearch) || c.ParentCategory.Name.Contains(sSearch));
                }
            }
            var allCategories = query;
            IEnumerable<GalleryItemCategory> filteredCategories = allCategories;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredCategories = filteredCategories.OrderBy(c => c.Id);
                        break;
                    case 2:
                        filteredCategories = filteredCategories.OrderBy(c => c.Name);
                        break;
                    case 3:
                        filteredCategories = filteredCategories.OrderBy(c => c.ParentCategory.Name);
                        break;
                    default:
                        filteredCategories = filteredCategories.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredCategories = filteredCategories.OrderByDescending(c => c.Id);
                        break;
                    case 2:
                        filteredCategories = filteredCategories.OrderByDescending(c => c.Name);
                        break;
                    case 3:
                        filteredCategories = filteredCategories.OrderByDescending(c => c.ParentCategory.Name);
                        break;
                    default:
                        filteredCategories = filteredCategories.OrderByDescending(c => c.Id);
                        break;
                }

            }
            var displayedCategories = filteredCategories.Skip(displayStart);
            if (displayLength >= 0)
            {
                displayedCategories = displayedCategories.Take(displayLength);
            }
            totalRecords = allCategories.Count();
            totalDisplayRecords = filteredCategories.Count();
            return displayedCategories.ToList();
        }


    }
    public interface IGalleryItemCategoryRepository : IRepository<GalleryItemCategory>
    {
        IEnumerable<GalleryItemCategory> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);

    }
}
