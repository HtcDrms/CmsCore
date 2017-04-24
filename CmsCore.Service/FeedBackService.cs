using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Service
{

    public interface IFeedbackService
    {
        IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        IEnumerable<Feedback> GetFeedbacks();
        List<FeedbackValue> GetFeedbackValueByFeedbackId(long id);
        List<Feedback> GetFeedbacks(string User, int id);
        Feedback GetFeedback(int id);
        void FeedbackPost(IFormCollection collection, string filePath);
        void FeedbackPostMail(string body, long id);
        void CreateFeedback(Feedback Feedback);
        void UpdateFeedback(Feedback Feedback);
        void DeleteFeedback(long id);
        int CountFeedback();
        void SaveFeedback();

    }
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository feedbackRepository;
        private readonly IFormService formService;
        private readonly ISettingService settingService;
        private readonly IUnitOfWork unitOfWork;
        public FeedbackService(IFeedbackRepository FeedbackRepository, IUnitOfWork unitOfWork, IFormService formService,ISettingService settingService)
        {
            this.feedbackRepository = FeedbackRepository;
            this.unitOfWork = unitOfWork;
            this.formService = formService;
            this.settingService = settingService;
        }
        #region IFeedbackService Members

        public void FeedbackPost(IFormCollection collection, string filePath)
        {
            int i = 3;
            Feedback feed_back = new Feedback();
            var form = formService.GetForm(Convert.ToInt64(collection["FormId"]));
            var body = "";
            foreach (var item in formService.GetFormFieldsByFormId(Convert.ToInt64(collection["FormId"])))
            {
                var feedBackValue = new FeedbackValue();

                feedBackValue.FormFieldName = item.Name;
                feedBackValue.FieldType = item.FieldType;
                feedBackValue.FormFieldId = (int)item.Id;
                feedBackValue.Position = item.Position;
                feedBackValue.AddedBy = "username";
                feedBackValue.AddedDate = DateTime.Now;
                feedBackValue.ModifiedBy = "username";
                feedBackValue.ModifiedDate = DateTime.Now;
                foreach (var item2 in collection)
                {
                    if (item.Name == item2.Key)
                    {
                        feedBackValue.Value = item2.Value;
                        body = body + item2.Key + " : " + item2.Value + "<br/>";
                    }
                }

                feed_back.FeedbackValues.Add(feedBackValue);
            }
            //feedBack.IP = GetUserIP();  // gönderen ip method u eklenecek

            //var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;


            feed_back.FormId = (int)form.Id;
            feed_back.FormName = form.FormName;

            //feed_back.IP = remoteIpAddress.ToString();
            feed_back.SentDate = DateTime.Now;
            feed_back.UserName = "username";
            feed_back.AddedBy = "username";
            feed_back.AddedDate = DateTime.Now;
            feed_back.ModifiedDate = DateTime.Now;
            feed_back.ModifiedBy = "username";

            CreateFeedback(feed_back);
            SaveFeedback();

            FeedbackPostMail(body, form.Id);
            //return feed_back.FeedbackValues.ToList();

        }

        public void FeedbackPostMail(string body, long id)
        {
            var form = formService.GetForm(id);
            if (form.EmailBcc != null || form.EmailCc != null || form.EmailTo != null)
            {
                var message = new MimeMessage();
                if (form.EmailCc != null)
                {
                    var email_cc_list = form.EmailCc.Split(',');
                    foreach (var item2 in email_cc_list)
                    {
                        message.Cc.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                if (form.EmailBcc != null)
                {
                    var email_bcc_list = form.EmailBcc.Split(',');
                    foreach (var item2 in email_bcc_list)
                    {
                        message.Bcc.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                if (form.EmailTo != null)
                {
                    var email_to_list = form.EmailTo.Split(',');
                    foreach (var item2 in email_to_list)
                    {
                        message.To.Add(new MailboxAddress(item2.Trim(), item2.Trim()));
                    }
                }
                message.From.Add(new MailboxAddress("CMS Core", settingService.GetSettingByName("Email").Value));
                var bodyBuilder = new BodyBuilder();
                message.Subject = "CMS Core " + form.FormName;
                // foreach (var item in feed_back.FeedbackValues)
                //{
                //message.Body += EmailString(item).ToString() + "<br/>";
                bodyBuilder.HtmlBody += body;
                //}
                message.Body = bodyBuilder.ToMessageBody();
                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.
                        client.Authenticate(settingService.GetSettingByName("Email").Value, settingService.GetSettingByName("EmailPassword").Value);
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Feedback> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var Feedbacks = feedbackRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);

            return Feedbacks;
        }
        public IEnumerable<Feedback> GetFeedbacks()
        {
            var Feedbacks = feedbackRepository.GetAll("Form");
            return Feedbacks;
        }
        public Feedback GetFeedback(int id)
        {
            var Feedback = feedbackRepository.GetById(id);
            return Feedback;
        }
        public List<FeedbackValue> GetFeedbackValueByFeedbackId(long id)
        {
            Feedback feedback = feedbackRepository.GetById(id, "FeedbackValues");
            return feedback.FeedbackValues.OrderBy(c => c.Position).ToList();
        }
        public void CreateFeedback(Feedback Feedback)
        {
            feedbackRepository.Add(Feedback);
        }
        public void UpdateFeedback(Feedback Feedback)
        {
            feedbackRepository.Update(Feedback);
        }
        public void DeleteFeedback(long id)
        {
            feedbackRepository.Delete(f => f.Id == id);
        }
        public void SaveFeedback()
        {
            unitOfWork.Commit();
        }
        public int CountFeedback()
        {
            return feedbackRepository.GetAll().Count();
        }

        public List<Feedback> GetFeedbacks(string User, int id)
        {
            return feedbackRepository.GetMany(c => c.UserName == User && c.FormId == id).ToList();
        }
        #endregion
    }

}
