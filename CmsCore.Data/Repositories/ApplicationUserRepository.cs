using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Data.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext DbContext;
        public ApplicationUserRepository(ApplicationDbContext dbContext)
                { this.DbContext = dbContext; }
        public IEnumerable<ApplicationUser> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');

            var query = this.DbContext.Users.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    //query = query.Where(p => p.UserName.Contains(sSearch));
                    query = query.Where(p => p.UserName.Contains(search));
                }
            }

            var allApplicationUser = query;
            IEnumerable<ApplicationUser> filteredApplicationUser = allApplicationUser;
            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredApplicationUser = filteredApplicationUser.OrderBy(p => p.Email);
                        break;
                    case 2:
                        filteredApplicationUser = filteredApplicationUser.OrderBy(p => p.Email);
                        break;
                    default:
                        filteredApplicationUser = filteredApplicationUser.OrderBy(c => c.Email);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 1:
                        filteredApplicationUser = filteredApplicationUser.OrderByDescending(p => p.Email);
                        break;
                    case 2:
                        filteredApplicationUser = filteredApplicationUser.OrderByDescending(p => p.Email);
                        break;
                    default:
                        filteredApplicationUser = filteredApplicationUser.OrderByDescending(c => c.Email);
                        break;
                }
            }
            var displayedApplicationUser = filteredApplicationUser.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedApplicationUser = displayedApplicationUser.Take(displayLength);
            }
            totalRecords = allApplicationUser.Count();
            totalDisplayRecords = filteredApplicationUser.Count();
            return displayedApplicationUser.ToList();
        }

    }
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}
