using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.EntityBuilders
{
    public class SlideBuilder
    {
        public SlideBuilder(EntityTypeBuilder<Slide> entityBuilder)
        {
            entityBuilder.HasKey(s => s.Id);
            entityBuilder.HasOne(s => s.Slider).WithMany(sr => sr.Slides).HasForeignKey(s => s.SliderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}