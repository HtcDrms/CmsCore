using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.Entities
{
    public class GalleryItem:BaseEntity
    {
        public GalleryItem()
        {
            IsPublished = true;
            GalleryItemGalleryItemCategories = new HashSet<GalleryItemGalleryItemCategory>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public string Photo { get; set; }
        public string Video { get; set; }
        public string Meta1 { get; set; }
        public long GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public bool IsPublished { get; set; }        
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }
    }
}
