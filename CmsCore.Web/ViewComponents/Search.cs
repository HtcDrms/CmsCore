using CmsCore.Model.Dtos;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakura.AspNetCore;

namespace CmsCore.Web.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly ISearchService searchService;

        public Search(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int pageNumber = 1;
            var pageSize = 6;
            List<SearchDto> searchresult = null;
            var ww = Request.Query["query"].ToString();


            if (ww!=null && ww!="")
            {
                if (!String.IsNullOrEmpty(Request.Query["page"]))
                {
                    pageNumber = Convert.ToInt32(Request.Query["page"]);
                }                
                searchresult = await GetSearch(ww);
                var pagedData = searchresult.ToPagedList(pageSize, pageNumber);
                return View(pagedData);
            }            
            return View("Default",new List<SearchDto>().ToPagedList(pageSize,1));

        }
        private Task<List<SearchDto>> GetSearch(string word)
        {
            return Task.FromResult(searchService.Search(word));
        }

    }
}
