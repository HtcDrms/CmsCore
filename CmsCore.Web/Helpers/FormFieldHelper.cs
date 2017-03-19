using CmsCore.Model.Entities;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CmsCore.Web.Helpers
{
    [HtmlTargetElement("formField", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FormFieldHelper : TagHelper
    {
        public FormField formField { get; set; }
        public bool required { get; set; }
        public bool read_only { get; set; }
        public bool showPlaceholder { get; set; }
        public bool showLabel { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";
            // output.Attributes.SetAttribute("class", "form-control");

            if (formField.FieldType == FieldType.fullName || formField.FieldType == FieldType.smallText)
            {
                TagBuilder textbox = new TagBuilder("input");
                textbox.Attributes.Add("type", "text");
                textbox.Attributes.Add("name", formField.Name);
                if (showPlaceholder)
                {
                    textbox.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    if (showLabel) { 
                     output.Content.SetContent(formField.Name + "(*)");
                    }
                    textbox.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        textbox.Attributes.Add("value", formField.Value.ToString());
                    }
                    textbox.Attributes.Add("data-val", "true");
                    textbox.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                else
                {
                    if (showLabel)
                    {
                        output.Content.SetContent(formField.Name);
                    }
                    if (formField.Value != null)
                    {
                        textbox.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        textbox.Attributes.Add("value", "");
                    }
                }
                textbox.MergeAttribute("class", "form-control spinner");
                textbox.Attributes.Add("width", "100%");
                if (read_only == true)
                {
                    textbox.Attributes.Add("readonly", "readonly");
                }

                var writer = new System.IO.StringWriter();
                textbox.WriteTo(writer, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent((showLabel?"<br/>":"") + writer.ToString());
            }
            else if (formField.FieldType == FieldType.largeText)
            {
                TagBuilder textboxArea = new TagBuilder("textarea");
                textboxArea.Attributes.Add("type", "text");
                textboxArea.Attributes.Add("name", formField.Name);
                if (showPlaceholder)
                {
                    textboxArea.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    if (showLabel)
                    {
                        output.Content.SetContent(formField.Name + "(*)");
                    }
                    textboxArea.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        textboxArea.Attributes.Add("value", formField.Value.ToString());
                    }
                    textboxArea.Attributes.Add("data-val", "true");
                    textboxArea.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                else
                {
                    if (showLabel)
                    {
                        output.Content.SetContent(formField.Name);
                    }
                    if (formField.Value != null)
                    {
                        textboxArea.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        textboxArea.Attributes.Add("value", "");
                    }
                }
                textboxArea.MergeAttribute("class", "form-control spinner");
                textboxArea.Attributes.Add("width", "100%");
                if (read_only == true)
                {
                    textboxArea.Attributes.Add("readonly", "readonly");
                }

                var writer = new System.IO.StringWriter();
                textboxArea.WriteTo(writer, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent("<br/>" + writer.ToString());
            }
            else if (formField.FieldType == FieldType.dropdownMenu)
            {
               
                TagBuilder text = new TagBuilder("text");
                
                text.InnerHtml.SetContent(formField.Name);
                
                TagBuilder list = new TagBuilder("select");
                list.Attributes.Add("name", formField.Name);
                var items = formField.Value.Split(',');
                string element = "";
                foreach (var item in items)
                {
                    if (item.ToString().Length > 3)
                    {
                        if (item.ToString().Remove(3, item.Length - 3) == "(+)")
                        {
                            TagBuilder singlechoice = new TagBuilder("option");
                            singlechoice.Attributes.Add("value", item.ToString().Remove(0, 3));
                            singlechoice.Attributes.Add("selected", "selected");
                            singlechoice.InnerHtml.SetHtmlContent(item.ToString().Remove(0, 3));
                            var single = new System.IO.StringWriter();
                            singlechoice.WriteTo(single, HtmlEncoder.Default);
                            element += single.ToString() + "<br/>";
                        }
                        else
                        {
                            TagBuilder singlechoice = new TagBuilder("option");
                            singlechoice.Attributes.Add("value", item);
                            singlechoice.InnerHtml.SetHtmlContent(item);
                            var single = new System.IO.StringWriter();
                            singlechoice.WriteTo(single, HtmlEncoder.Default);
                            element += single.ToString() + "<br/>";
                        }
                    }
                    else
                    {
                        TagBuilder singlechoice = new TagBuilder("option");
                        singlechoice.Attributes.Add("value", item);
                        singlechoice.InnerHtml.SetHtmlContent(item);
                        var single = new System.IO.StringWriter();
                        singlechoice.WriteTo(single, HtmlEncoder.Default);
                        element += single.ToString() + "<br/>";
                    }
                }
                list.InnerHtml.SetHtmlContent(element);
                if (required == true)
                {
                    list.Attributes.Add("required", "required");
                    list.Attributes.Add("data-val", "true");
                    list.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                if (read_only == true)
                {
                    list.Attributes.Add("disabled", "disabled");
                }
                list.Attributes.Add("class", "form-control");
                
                var writer = new System.IO.StringWriter();
                list.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                text.WriteTo(writer2, HtmlEncoder.Default);
                

                output.PostContent.SetHtmlContent(writer2.ToString()+"<br/>" + writer.ToString());

            }
            else if (formField.FieldType == FieldType.checkbox)
            {
                
                var items = formField.Value.Split(',');
                string element = "";
                int i = 0;
                foreach (var item in items)
                {
                    i++;
                    TagBuilder multiplechoice = new TagBuilder("input");
                    multiplechoice.Attributes.Add("type", "checkbox");
                    multiplechoice.Attributes.Add("class", "");
                    multiplechoice.Attributes.Add("name", formField.Name + i.ToString());
                    if (item.ToString().Length > 3)
                    {
                        if (item.ToString().Remove(3, item.Length - 3) == "(+)")
                        {
                            multiplechoice.Attributes.Add("value", item.ToString().Remove(0, 3));
                            multiplechoice.Attributes.Add("checked", "checked");
                            multiplechoice.InnerHtml.SetHtmlContent(item.ToString().Remove(0, 3));
                        }
                        else
                        {
                            multiplechoice.Attributes.Add("value", item);
                            multiplechoice.InnerHtml.SetHtmlContent(item);
                        }
                    }
                    else
                    {
                        multiplechoice.Attributes.Add("value", item);
                        multiplechoice.Attributes.Add("style", "margin-right:5px;");
                        multiplechoice.InnerHtml.SetHtmlContent(item);
                    }
                    if (read_only == true)
                    {
                        multiplechoice.Attributes.Add("disabled", "disabled");
                    }

                    var multi = new System.IO.StringWriter();
                    multiplechoice.WriteTo(multi, HtmlEncoder.Default);
                    element += multi.ToString() + "<br/>";
                }
                element = (showLabel?formField.Name:"") + element;
                
                output.PostContent.SetHtmlContent((showLabel ? "<br/>":"") + element.ToString());
            }
            else if (formField.FieldType == FieldType.email)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                //var items = formInfo.Value.Split(',');
                TagBuilder email = new TagBuilder("input");
                email.Attributes.Add("type", "email");
                email.Attributes.Add("multiple", "true");
                email.Attributes.Add("class", "form-control spinner");
                if (showPlaceholder)
                {
                    email.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    email.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        email.Attributes.Add("value", formField.Value.ToString());
                    }
                    email.Attributes.Add("data-val", "true");
                    email.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                    email.Attributes.Add("data-val-email", "Lütfen geçerli bir e-posta adresi giriniz.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        email.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        email.Attributes.Add("value", "");
                    }
                }
                if (read_only == true)
                {
                    email.Attributes.Add("readonly", "readonly");
                }
                email.Attributes.Add("width", "100%");
                email.Attributes.Add("name", formField.Name);

                var writer = new System.IO.StringWriter();
                email.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                text.WriteTo(writer2, HtmlEncoder.Default);
                output.PostContent.SetHtmlContent((showLabel?writer2.ToString() + "<br/>":"") + writer.ToString());

            }
            else if (formField.FieldType == FieldType.telephone)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                //var items = formInfo.Value.Split(',');
                TagBuilder email = new TagBuilder("input");
                email.Attributes.Add("type", "tel");
                email.Attributes.Add("class", "form-control spinner");
                if (showPlaceholder)
                {
                    email.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    email.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        email.Attributes.Add("value", formField.Value.ToString());
                    }
                    email.Attributes.Add("data-val", "true");
                    email.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                    email.Attributes.Add("data-val-phone", "Lütfen geçerli bir telefon numarası giriniz.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        email.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        email.Attributes.Add("value", "");
                    }
                }
                if (read_only == true)
                {
                    email.Attributes.Add("readonly", "readonly");
                }
                email.Attributes.Add("width", "100%");
                email.Attributes.Add("name", formField.Name);

                var writer = new System.IO.StringWriter();
                email.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                text.WriteTo(writer2, HtmlEncoder.Default);
                output.PostContent.SetHtmlContent((showLabel?writer2.ToString() + "<br/>":"") + writer.ToString());

            }
            else if (formField.FieldType == FieldType.file)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                TagBuilder file = new TagBuilder("input");
                file.Attributes.Add("type", "file");
                if (required == true)
                {
                    file.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        file.Attributes.Add("value", formField.Value.ToString());
                    }
                    file.Attributes.Add("data-val", "true");
                    file.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        file.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        file.Attributes.Add("value", "");
                    }
                }
                file.Attributes.Add("name", "upload");

                var writer = new System.IO.StringWriter();
                file.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                text.WriteTo(writer2, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent((showLabel?writer2.ToString() + "<br/>":"") + writer.ToString() + "<br/> Dosyanın uzantısı .doc, .docx, .pdf, .rtf, .jpg, .gif, .png olmalıdır.");

            }
            else if (formField.FieldType == FieldType.radioButtons)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                string element = "";
                var items = formField.Value.Split(',');
                bool firstItem = true;
                foreach (var item in items)
                {
                    TagBuilder singlechoice = new TagBuilder("input");
                    singlechoice.Attributes.Add("type", "radio");
                    singlechoice.Attributes.Add("name", formField.Name);
                    if (!firstItem) { 
                        singlechoice.Attributes.Add("style", "margin-left:5px;");
                    } else
                    {
                        firstItem = false;
                    }
                    if (item.ToString().Length > 3)
                    {
                        if (item.ToString().Remove(3, item.Length - 3) == "(+)")
                        {
                            singlechoice.Attributes.Add("value", item.ToString().Remove(0, 3));
                            singlechoice.Attributes.Add("checked", "checked");
                            singlechoice.InnerHtml.SetHtmlContent(item.ToString().Remove(0, 3));
                        }
                        else
                        {
                            singlechoice.Attributes.Add("value", item);
                            singlechoice.InnerHtml.SetHtmlContent(item);
                        }
                    }
                    else
                    {
                        singlechoice.Attributes.Add("value", item);
                        singlechoice.InnerHtml.SetHtmlContent(item);
                    }
                    if (read_only == true)
                    {
                        singlechoice.Attributes.Add("disabled", "disabled");
                    }

                    var writer2 = new System.IO.StringWriter();
                    singlechoice.WriteTo(writer2, HtmlEncoder.Default);

                    element += writer2.ToString();
                }
                var writer = new System.IO.StringWriter();
                text.WriteTo(writer, HtmlEncoder.Default);
                output.PostContent.SetHtmlContent(writer.ToString() + "<br/>" + element.ToString());
            }
            else if (formField.FieldType == FieldType.datePicker)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                TagBuilder date = new TagBuilder("input");
                date.Attributes.Add("type", "date");
                if (showPlaceholder)
                {
                    date.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    date.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        date.Attributes.Add("value", formField.Value.ToString());
                    }
                    date.Attributes.Add("data-val", "true");
                    date.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        date.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        date.Attributes.Add("value", "");
                    }
                }
                date.Attributes.Add("class", "form-control spinner");
                date.Attributes.Add("name", formField.Name);
                if (read_only == true)
                {
                    date.Attributes.Add("readonly", "readonly");
                }
                var writer = new System.IO.StringWriter();
                text.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                date.WriteTo(writer2, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent((showLabel?writer.ToString() + "<br/>":"") + writer2.ToString());
            }
            else if (formField.FieldType == FieldType.urlWebSite)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                TagBuilder url = new TagBuilder("input");
                if (formField.Value != null)
                {
                    url.Attributes.Add("type", "url");
                }
                if (showPlaceholder)
                {
                    url.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    url.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        url.Attributes.Add("value", formField.Value.ToString());
                    }
                    url.Attributes.Add("data-val", "true");
                    url.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                    url.Attributes.Add("data-val-url", "Lütfen geçerli bir web adresi giriniz.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        url.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        url.Attributes.Add("value", "");
                    }
                }
                if (read_only == true)
                {
                    url.Attributes.Add("readonly", "readonly");
                }
                url.Attributes.Add("width", "100%");
                url.Attributes.Add("class", "form-control spinner");
                url.Attributes.Add("name", formField.Name);

                var writer = new System.IO.StringWriter();
                text.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                url.WriteTo(writer2, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent((showLabel?writer.ToString() + "<br/>":"") + writer2.ToString());

            }
            else if (formField.FieldType == FieldType.numberValue)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                TagBuilder number = new TagBuilder("input");
                number.Attributes.Add("type", "number");
                if (showPlaceholder)
                {
                    number.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    number.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        number.Attributes.Add("value", formField.Value.ToString());
                    }
                    number.Attributes.Add("data-val", "true");
                    number.Attributes.Add("data-val-required", "Lütfen geçerli bir değer giriniz.");
                    //number.Attributes.Add("data-val-number", "Lütfen geçerli bir sayı giriniz.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        number.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        number.Attributes.Add("value", "");
                    }
                }
                if (read_only == true)
                {
                    number.Attributes.Add("readonly", "readonly");
                }
                number.Attributes.Add("class", "form-control spinner");
                number.Attributes.Add("width", "100%");
                number.Attributes.Add("name", formField.Name);

                var writer = new System.IO.StringWriter();
                text.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                number.WriteTo(writer2, HtmlEncoder.Default);

                output.PostContent.SetHtmlContent((showLabel?writer.ToString() + "<br/>":"") + writer2.ToString());
            }
            else if (formField.FieldType == FieldType.timeValue)
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetContent(formField.Name);
                TagBuilder time = new TagBuilder("input");
                time.Attributes.Add("type", "time");
                if (showPlaceholder)
                {
                    time.Attributes.Add("placeholder", formField.Name);
                }
                if (required == true)
                {
                    text.InnerHtml.SetContent(formField.Name + "(*)");
                    time.Attributes.Add("required", "required");
                    if (formField.Value != null)
                    {
                        time.Attributes.Add("value", formField.Value.ToString());
                    }

                    time.Attributes.Add("data-val", "true");
                    time.Attributes.Add("data-val-required", "Lütfen bu alanı boş bırakmayınız.");
                }
                else
                {
                    if (formField.Value != null)
                    {
                        time.Attributes.Add("value", formField.Value.ToString());
                    }
                    else
                    {
                        time.Attributes.Add("value", "");
                    }
                }


                if (read_only == true)
                {
                    time.Attributes.Add("readonly", "readonly");
                }
                time.Attributes.Add("class", "form-control spinner");
                time.Attributes.Add("name", formField.Name);

                var writer = new System.IO.StringWriter();
                text.WriteTo(writer, HtmlEncoder.Default);

                var writer2 = new System.IO.StringWriter();
                time.WriteTo(writer2, HtmlEncoder.Default);
                output.PostContent.SetHtmlContent((showLabel?writer.ToString() + "<br/>":"") + writer2.ToString());
            }
            else
            {
                output.PostContent.SetHtmlContent("");
            }
        }
    }
}
