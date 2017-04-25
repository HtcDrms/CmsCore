using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Model.Entities
{
    public class Template:BaseEntity
    {
        public Template()
        {
            TemplateSections = new HashSet<TemplateSection>();
            Pages = new HashSet<Page>();
            Sliders = new HashSet<Slider>();
        }
       
        public string Name { get; set; }
        public string ViewName { get; set; }
        public virtual ICollection<TemplateSection> TemplateSections { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<Slider> Sliders { get; set; }
        public virtual ICollection<Form> Forms { get; set; }
    }
}
