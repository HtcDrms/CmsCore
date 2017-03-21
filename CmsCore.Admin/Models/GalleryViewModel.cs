using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Models
{
    public class GalleryViewModel : BaseEntity
    {
        public GalleryViewModel()
        {
            IsPublished = true;
            GalleryItems = new HashSet<GalleryItem>();
        }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<GalleryItem> GalleryItems { get; set; }
    }
}
