using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Models
{
   public class SliderViewModel:BaseEntity
    {
        public SliderViewModel()
        {
            IsPublished = true;
            Slides = new HashSet<Slide>();
        }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public virtual ICollection<Slide> Slides { get; set; }
        public long? TemplateId { get; set; }
        public virtual Template Template { get; set; }
    }
}
