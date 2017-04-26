using CmsCore.Data;
using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Dtos;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmsCore.Service
{
    public interface ISearchService
    {
        List<SearchDto> Search(string search);
    }
    public class SearchService : ISearchService
    {
        private readonly IPostRepository postRepository;
        private readonly IPageRepository pageRepository;
        private readonly IUnitOfWork unitOfWork;

        public SearchService(IPostRepository postRepository, IUnitOfWork unitOfWork, IPageRepository pageRepository)
        {
            this.postRepository = postRepository;
            this.pageRepository = pageRepository;
            this.unitOfWork = unitOfWork;
        }
        public List<SearchDto> Search(string search)
        {
            List<SearchDto> searchDto = new List<SearchDto>();
            searchDto.AddRange(postRepository.Search(search));
            searchDto.AddRange(pageRepository.Search(search));
            searchDto.AsQueryable().OrderByDescending(v => v.ViewCount);
            return searchDto;
        }
    }
}
