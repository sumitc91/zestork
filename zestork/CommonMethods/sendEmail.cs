using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;

namespace zestork.CommonMethods
{
    public class sendEmail
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        
        String path;
        MailMessage mail = new MailMessage();

        public string sendEmailMessage(String toEmailAddrList,String senderName,String subject,String body,String attachmentsFilePathList,String logoPath, String companyDescription)
        {
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpEmail"].ToString(), ConfigurationManager.AppSettings["SmtpPassword"].ToString());
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            mail = new MailMessage();
            String[] addr = toEmailAddrList.Split(',');
            try
            {
                mail.From = new MailAddress(ConfigurationManager.AppSettings["SmtpEmail"].ToString(), senderName, System.Text.Encoding.UTF8);
                Byte i;
                for (i = 0; i < addr.Length; i++)
                    mail.To.Add(addr[i]);
                mail.Subject = subject;
                mail.Body = body;
                if (attachmentsFilePathList != null)
                {
                    String[] attachments = attachmentsFilePathList.Split(',');
                    for (i = 0; i < attachments.Length; i++)
                        mail.Attachments.Add(new Attachment(attachments[i]));
                }                
                path = logoPath;
                string htmlview;
                if (path != null)
                {
                    LinkedResource logo = new LinkedResource(path);
                    logo.ContentId = "Logo";
                    htmlview = "<html><body><table border=2><tr width=100%><td><img src=cid:Logo alt=companyname /></td><td>" + companyDescription + "</td></tr></table><hr/></body></html>";
                    AlternateView alternateView1 = AlternateView.CreateAlternateViewFromString(htmlview + body, null, MediaTypeNames.Text.Html);
                    alternateView1.LinkedResources.Add(logo);
                    mail.AlternateViews.Add(alternateView1);
                }
                else
                {
                    AlternateView alternateView1 = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);                    
                    mail.AlternateViews.Add(alternateView1);
                }
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mail.ReplyToList = new MailAddress(ConfigurationManager.AppSettings["SmtpEmail"].ToString());
                SmtpServer.Send(mail);
                return "200";
            }
            catch (Exception ex)
            {
                logger.Error("Exception occured while sending email",ex);
                return "500";
            }
            
        }
    }
}