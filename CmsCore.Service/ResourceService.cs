using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Service
{
    public interface IResourceService
    {
        IEnumerable<Resource> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Resource> GetResources();
        Resource GetResource(long id);
        void CreateResource(Resource resource);
        void UpdateResource(Resource resource);
        void DeleteResource(Resource resource);
        void SaveResource();
    }
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository resourceRepository;
        private readonly IUnitOfWork unitOfWork;
        public ResourceService(IResourceRepository resourceRepository, IUnitOfWork unitOfWork)
        {
            this.resourceRepository = resourceRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IResourceService Members
        public IEnumerable<Resource> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var resources = resourceRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);

            return resources;
        }
        public IEnumerable<Resource> GetResources()
        {
            var resources = resourceRepository.GetAll("Name");
            return resources;
        }
        public Resource GetResource(long id)
        {
            var resource = resourceRepository.GetById(id);
            return resource;
        }
        public void CreateResource(Resource resource)
        {
            resourceRepository.Add(resource);
        }
        public void UpdateResource(Resource resource)
        {
            resourceRepository.Update(resource);
        }
        public void DeleteResource(Resource resource)
        {
            resourceRepository.Delete(resource);
        }
        public void SaveResource()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
