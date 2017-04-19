using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CmsCore.Model.Entities
{
    public class GalleryItemViewModel : BaseEntity
    {
        public GalleryItemViewModel()
        {
            IsPublished = true;
            Meta1 = "grid-item-height1";
        }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public string Photo { get; set; }
        public string categoriesHidden { get; set; }
        public string Video { get; set; }
        public string Meta1 { get; set; }
        public bool IsPublished { get; set; }
        public long GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ICollection<GalleryItemGalleryItemCategory> GalleryItemGalleryItemCategories { get; set; }

    }
}
