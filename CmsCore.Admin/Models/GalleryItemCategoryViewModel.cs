using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Models
{
    public class GalleryItemCategoryViewModel:BaseEntity
    {
        public GalleryItemCategoryViewModel()
        {
            ChildCategories = new HashSet<GalleryItemCategory>();
            GalleryItemGalleryItemCategories = new HashSet<GalleryItemGalleryItemCategory>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public long? ParentCategoryId { get; set; }
        public virtual GalleryItemCategory ParentCategory { get; set; }
        public virtual ICollection<GalleryItemCategory> ChildCategories { get; set; }
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }

    }
}
