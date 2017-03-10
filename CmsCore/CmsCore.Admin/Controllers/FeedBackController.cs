using CmsCore.Admin.Models;
using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using System.Net;
using MailKit.Net.Imap;
using MailKit.Security;
using System.Text.Encodings.Web;

namespace CmsCore.Admin.Controllers
{
    public class FeedBackController:BaseController
    {
        private readonly IFormService formService;
        private readonly IFeedbackService feedbackService;
        private readonly ISettingService settingService;
        private readonly IFormFieldService formFieldService;
        public FeedBackController(IFormService formService, ISettingService settingService, IFormFieldService formFieldService, IFeedbackService feedbackService)
        {
            this.formService = formService;
            this.settingService = settingService;
            this.formFieldService = formFieldService;
            this.feedbackService = feedbackService;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormViewModel form)
        {
            FeedbackViewModel feedback = new FeedbackViewModel();
            feedback.FormId = (int)form.Id;
            ViewBag.Form = formService.GetForm(feedback.Id);
            ViewBag.FormFields = new List<FormField>(formService.GetFormFieldsByFormId(form.Id));
            feedback.AddedDate = DateTime.Now;
            return View(feedback);
        }

        [HttpPost]
        public ActionResult Post(FeedbackViewModel feedBack, IFormCollection collection, IFormFile upload)
        {
            int i = 3;
            if (ModelState.IsValid)
            {
                List<FeedbackValue> FeedBackValues = new List<FeedbackValue>();
                var form = formService.GetForm((long)feedBack.FormId);
                foreach (var item in formService.GetFormFieldsByFormId((long)feedBack.FormId))
                {
                    var feedBackValue = new FeedbackValue();
                    feedBackValue.FormFieldName = item.Name;
                    feedBackValue.FieldType = item.FieldType;
                    feedBackValue.FormFieldId = (int)item.Id;
                    feedBackValue.Position = item.Position;
                    feedBackValue.Value = item.Value;
                    feedBackValue.AddedBy = User.Identity.Name??"username";
                    feedBackValue.AddedDate = DateTime.Now;
                    feedBackValue.ModifiedBy = User.Identity.Name ?? "username";
                    feedBackValue.ModifiedDate = DateTime.Now;

                    if (item.FieldType.ToString() == "multipleChoice" || item.FieldType.ToString() == "radioButtons" || item.FieldType.ToString() == "singleChoice")
                    {
                        feedBackValue.Value = "";
                        var choices = item.Value.Split(',');
                        foreach (var choice in choices)
                        {
                            if (i < collection.Count && choice == collection[i.ToString()])
                            {
                                feedBackValue.Value += "(+)" + collection[i.ToString()] + ",";
                                i++;
                            }
                            else if (i < collection.Count)
                            {
                                feedBackValue.Value += choice.ToString() + ",";
                            }
                        }
                        feedBackValue.Value = feedBackValue.Value.Remove(feedBackValue.Value.Length - 1);
                    }
                    else if (item.FieldType.ToString() == "file")
                    {
                        string FilePath = ViewBag.UploadPath + "\\feedback\\";
                        string dosyaYolu = Path.GetFileName(upload.FileName);
                        var yuklemeYeri = Path.Combine(FilePath + dosyaYolu);
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eğer klasör yoksa oluştur
                                upload.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                upload.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        if (i < collection.Count)
                        {
                            //feedBackValue.Value = collection[i.ToString()];
                            i++;
                        }
                    }
                    FeedBackValues.Add(feedBackValue);
                }

                //feedBack.IP = GetUserIP();  // gönderen ip method u eklenecek

                feedBack.SentDate = DateTime.Now;
                if (User.Identity.Name != null)
                {
                    feedBack.UserName = User.Identity.Name;
                }
                feedBack.FormName = form.FormName;

                Feedback feed_back = new Feedback();
                feed_back.Id = feedBack.Id;
                feed_back.FormId = feedBack.FormId;
                feed_back.FormName = feedBack.FormName;
                feed_back.FeedbackValues = feedBack.FeedbackValues;
                feed_back.IP = feedBack.IP;
                feed_back.SentDate = DateTime.Now;

                feed_back.UserName = User.Identity.Name ?? "username";
                feed_back.AddedBy = User.Identity.Name ?? "username";
                feed_back.AddedDate = DateTime.Now;
                feed_back.ModifiedDate = DateTime.Now;
                feed_back.ModifiedBy = User.Identity.Name ?? "username";
                feed_back.FeedbackValues = FeedBackValues;
                
                feedbackService.CreateFeedback(feed_back);
                feedbackService.SaveFeedback();

                var userName = feedBack.UserName;
                var formId = feedBack.FormId;

                // MESAJ GÖNDERİMİ BURAYA GELECEK
                if (form.EmailBcc != null || form.EmailCc != null || form.EmailTo != null)
                {
                    var message = new MimeMessage();
                    if (form.EmailCc != null)
                    {
                        var email_cc_list = form.EmailCc.Split(',');
                        foreach (var item2 in email_cc_list)
                        {
                            message.Cc.Add(new MailboxAddress(item2.Trim(),item2.Trim()));
                        }
                    }
                    if (form.EmailBcc != null)
                    {
                        var email_bcc_list = form.EmailBcc.Split(',');
                        foreach (var item2 in email_bcc_list)
                        {
                            message.Bcc.Add(new MailboxAddress(item2.Trim(),item2.Trim()));
                        }
                    }
                    if (form.EmailTo != null)
                    {
                        var email_to_list = form.EmailTo.Split(',');
                        foreach (var item2 in email_to_list)
                        {
                            message.To.Add(new MailboxAddress(item2.Trim(),item2.Trim()));
                        }
                    }
                    message.From.Add(new MailboxAddress("CMS Core","ertyeni@gmail.com"));
                    var bodyBuilder = new BodyBuilder();
                    message.Subject = "CMS Core " + feedBack.FormName; 
                    foreach (var item in feed_back.FeedbackValues)
                    {
                        //message.Body += EmailString(item).ToString() + "<br/>";
                        bodyBuilder.HtmlBody += EmailString(item);
                    }
                    message.Body = bodyBuilder.ToMessageBody();
                    try
                    {
                        using (var client = new SmtpClient())
                        {
                            client.Connect("smtp.gmail.com", 587, false);
                            client.AuthenticationMechanisms.Remove("XOAUTH2");
                            // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.
                            client.Authenticate("ertyeni@gmail.com", "48448300+");
                            client.Send(message);
                            client.Disconnect(true);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                    return RedirectToAction("Posted", "Feedback", new { id = formId });
            }


            return RedirectToAction("Index","Feedback");
        }
        public string EmailString(FeedbackValue feedbackvalue)
        {
            if (feedbackvalue.FieldType.ToString() == "multipleChoice" || feedbackvalue.FieldType.ToString() == "singleChoice" || feedbackvalue.FieldType.ToString() == "radioButtons")
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetHtmlContent(feedbackvalue.FormFieldName);
                TagBuilder list = new TagBuilder("ul");
                var items = feedbackvalue.Value.Split(',');
                string element = "";
                foreach (var item in items)
                {
                    TagBuilder singlechoice = new TagBuilder("li");
                    singlechoice.Attributes.Add("value", item);
                    singlechoice.InnerHtml.SetHtmlContent(item);
                    element += singlechoice.ToString() + "<br/>";
                }
                list.InnerHtml.SetHtmlContent(element);

                var write = new System.IO.StringWriter();
                text.WriteTo(write, HtmlEncoder.Default);
                var write2 = new System.IO.StringWriter();
                list.WriteTo(write2, HtmlEncoder.Default);

                return (text.ToString() + "<br/>" + write2.ToString() + "<br/>");
            }
            else
            {
                TagBuilder text = new TagBuilder("text");
                text.InnerHtml.SetHtmlContent(feedbackvalue.FormFieldName);
                TagBuilder text2 = new TagBuilder("text");
                text2.InnerHtml.SetHtmlContent(feedbackvalue.Value);

                var write = new System.IO.StringWriter();
                text.WriteTo(write, HtmlEncoder.Default);
                var write2 = new System.IO.StringWriter();
                text2.WriteTo(write2, HtmlEncoder.Default);

                return (write.ToString() + ":" + write2.ToString() +"<br/>");
                
            }
        }

        public ActionResult Posted(int id)
        {
            ViewBag.Form = formService.GetForm(id);
            return View();
        }
    }
}
