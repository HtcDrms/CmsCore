using CmsCore.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Models
{
    public class ResourceViewModel:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public long LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
