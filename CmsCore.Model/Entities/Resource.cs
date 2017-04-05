using System;
using System.Collections.Generic;
using System.Text;

namespace CmsCore.Model.Entities
{
    public class Resource:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public long LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
