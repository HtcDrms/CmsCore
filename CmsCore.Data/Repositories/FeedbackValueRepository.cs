using CmsCore.Data.Infrastructure;
using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data.Repositories
{
    public class FeedbackValueRepository: RepositoryBase<FeedbackValue>, IFeedbackValueRepository
    {
        public FeedbackValueRepository(ApplicationDbContext dbFactory) : base(dbFactory) { }
    }
    public interface IFeedbackValueRepository : IRepository<FeedbackValue>
    {
    }
}
