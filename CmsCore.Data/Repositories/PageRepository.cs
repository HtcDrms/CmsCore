﻿using CmsCore.Data.Infrastructure;
using CmsCore.Model.Dtos;
using CmsCore.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CmsCore.Data.Repositories
{
    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(ApplicationDbContext dbContext)
                : base(dbContext) { }  
        
        public Page GetBySlug(string slug)
        {
            slug = slug.ToLower();
            return Get(p => p.Slug == slug,"Template");
        }

        public List<SearchDto> Search(string words)
        {
            words = words.Trim();
            var searchWords = words.Split(' ');
            var query = DbContext.Pages.AsQueryable();
            List<SearchDto> dto = new List<SearchDto>();
            foreach (string word in searchWords)
            {
                if (word != null && word != "")
                {
                    dto = query.Where(w => w.IsPublished == true).Where(p => p.Body.Contains(word) || p.Title.Contains(word) || p.SeoDescription.Contains(word) || p.SeoKeywords.Contains(word) || p.SeoKeywords.Contains(word)).Select(s => new SearchDto { Title = s.Title, Slug = s.Slug,ViewCount=s.ViewCount }).ToList();
                }
            }
            return dto;
        }

        public IEnumerable<Page> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            search = search.Trim();
            var searchWords = search.Split(' ');

            var query = this.DbContext.Pages.AsQueryable();
            foreach (string sSearch in searchWords)
            {
                if (sSearch != null && sSearch != "")
                {
                    query = query.Where(p => p.Title.Contains(sSearch) || p.AddedBy.Contains(sSearch) || p.ViewCount.ToString().Contains(sSearch) || p.ModifiedDate.ToString().Contains(sSearch));
                }
            }

            var allPages = query;



            IEnumerable<Page> filteredPages = allPages;



            if (sortDirection == "asc")
            {
                switch (sortColumnIndex)
                {

                    case 0:
                        filteredPages = filteredPages.OrderBy(p => p.Title);
                        break;
                    case 1:
                        filteredPages = filteredPages.OrderBy(p => p.AddedBy);
                        break;
                    case 2:
                        filteredPages = filteredPages.OrderBy(c => c.ViewCount);
                        break;
                    case 3:
                        filteredPages = filteredPages.OrderBy(c => c.ModifiedDate);
                        break;
                    default:
                        filteredPages = filteredPages.OrderBy(c => c.Title);
                        break;
                }
            }
            else
            {
                switch (sortColumnIndex)
                {
                    case 0:
                        filteredPages = filteredPages.OrderByDescending(p => p.Title);
                        break;
                    case 1:
                        filteredPages = filteredPages.OrderByDescending(p => p.AddedBy);
                        break;
                    case 2:
                        filteredPages = filteredPages.OrderByDescending(c => c.ViewCount);
                        break;
                    case 3:
                        filteredPages = filteredPages.OrderByDescending(c => c.ModifiedDate);
                        break;
                    default:
                        filteredPages = filteredPages.OrderByDescending(c => c.Title);
                        break;
                }
            }
            var displayedPages = filteredPages.Skip(displayStart);
            if (displayLength > 0)
            {
                displayedPages = displayedPages.Take(displayLength);
            }
            totalRecords = allPages.Count();
            totalDisplayRecords = filteredPages.Count();
            return displayedPages.ToList();
        }
    }
    public interface IPageRepository : IRepository<Page>
    {
        Page GetBySlug(string slug);
        List<SearchDto> Search(string words);
        IEnumerable<Page> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
    }
}


