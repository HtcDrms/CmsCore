using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.EntityBuilders
{
    public class GalleryItemCategoryBuilder
    {
        public GalleryItemCategoryBuilder(EntityTypeBuilder<GalleryItemCategory> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);
            entityBuilder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entityBuilder.HasOne(e => e.ParentCategory).WithMany(p => p.ChildCategories).HasForeignKey(p => p.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
