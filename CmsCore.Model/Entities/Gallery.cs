using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.Entities
{
    public class Gallery : BaseEntity
    {
        public Gallery()
        {
            IsPublished = true;
            GalleryItems = new HashSet<GalleryItem>();
        }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
