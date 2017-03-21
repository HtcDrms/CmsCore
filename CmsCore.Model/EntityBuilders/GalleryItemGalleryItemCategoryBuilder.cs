using CmsCore.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.EntityBuilders
{
    public class GalleryItemGalleryItemCategoryBuilder
    {
        public GalleryItemGalleryItemCategoryBuilder(EntityTypeBuilder<GalleryItemGalleryItemCategory> entityBuilder)
        {
            entityBuilder.HasKey(pc => new { pc.GalleryItemId, pc.GalleryItemCategoryId });

            entityBuilder.HasOne(bc => bc.GalleryItem)
                .WithMany(b => b.GalleryItemGalleryItemCategories)
                .HasForeignKey(bc => bc.GalleryItemId);

            entityBuilder.HasOne(bc => bc.GalleryItemCategory)
                .WithMany(c => c.GalleryItemGalleryItemCategories)
                .HasForeignKey(bc => bc.GalleryItemCategoryId);
        }
    }
}
