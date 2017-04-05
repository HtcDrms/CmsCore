using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Data.Repositories
{
    public class ResourceRepository : RepositoryBase<Resource>, IResourceRepository
    {
        public ResourceRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }
        public IEnumerable<Resource> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');


            var query = this.DbContext.Resources.Include("Language").AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(c => c.Name.Contains(sSearch)
                    || c.Value.Contains(sSearch));
                }
            }

            var allResources = query;

            IEnumerable<Resource> filteredResources = allResources;

            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredResources = filteredResources.OrderBy(c => c.Id);
                        break;
                    case 2:
                        filteredResources = filteredResources.OrderBy(c => c.Name);
                        break;
                    case 3:
                        filteredResources = filteredResources.OrderBy(c => c.Value);
                        break;
                    case 4:
                        filteredResources = filteredResources.OrderBy(c => c.LanguageId);
                        break;
                    default:
                        filteredResources = filteredResources.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredResources = filteredResources.OrderByDescending(c => c.Id);
                        break;
                    case 2:
                        filteredResources = filteredResources.OrderByDescending(c => c.Name);
                        break;
                    case 3:
                        filteredResources = filteredResources.OrderByDescending(c => c.Value);
                        break;
                    case 4:
                        filteredResources = filteredResources.OrderByDescending(c => c.LanguageId);
                        break;
                    default:
                        filteredResources = filteredResources.OrderByDescending(c => c.Id);
                        break;
                }
            }

            var displayedResources = filteredResources.Skip(displayStart);
            if (displayLength >= 0)
            {
                displayedResources = displayedResources.Take(displayLength);
            }

            totalRecords = allResources.Count();
            totalDisplayRecords = filteredResources.Count();
            return displayedResources.ToList();
        }
    }
    public interface IResourceRepository : IRepository<Resource>
    {
        IEnumerable<Resource> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);

    }
}
