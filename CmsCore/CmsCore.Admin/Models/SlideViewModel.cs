using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Models
{
    public class SlideViewModel:BaseEntity
    {
        public SlideViewModel()
        {
            IsPublished = true;
        }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public string Photo { get; set; }
        public string Video { get; set; }
        public string CallToActionText { get; set; }
        public string CallToActionUrl { get; set; }
        public bool IsPublished { get; set; }
        public long SliderId { get; set; }
        public virtual Slider Slider { get; set; }
    }
}
