using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CmsCore.Admin.Helpers
{
    [HtmlTargetElement("checkBoxList", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CheckBoxListHelper:TagHelper
    {
        public string Name { get; set; }
        public string Class { get; set; }
      
        public MultiSelectList Items { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";
            
            string group = "";
            foreach (var item in Items)
            {
                TagBuilder checkbox = new TagBuilder("input");
                checkbox.Attributes.Add("type", "checkbox");
                checkbox.Attributes.Add("name", Name);
                checkbox.Attributes.Add("class", Class);
                checkbox.Attributes.Add("value", item.Value);
                
                if (item.Disabled)                
                    checkbox.Attributes.Add("disabled", "disabled");
                
                if (item.Selected)
                    checkbox.Attributes.Add("checked", "checked");
                checkbox.InnerHtml.AppendHtml(item.Text);
                var writer = new System.IO.StringWriter();
                checkbox.WriteTo(writer, HtmlEncoder.Default);
                output.PostContent.AppendHtml((item.Group!=null?"&nbsp;&nbsp;&nbsp;":"").ToString()+writer.ToString()+"<br />");
                
            }                           
                
            }
        }
}
