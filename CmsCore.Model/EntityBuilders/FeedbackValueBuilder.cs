using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.EntityBuilders
{
    public class FeedbackValueBuilder
    {
        public FeedbackValueBuilder(EntityTypeBuilder<FeedbackValue> entityBuilder)
        {
            entityBuilder.HasKey(a => a.Id);
            entityBuilder.HasOne(f => f.Feedback).WithMany(fv => fv.FeedbackValues).HasForeignKey(f => f.FeedbackId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
