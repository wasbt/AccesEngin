using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class MailHelper
    {
        #region SendMail

        public static async Task SendEmail(List<string> To, string Body, string Subject = ConstsAccesEngin.AppName, string mailFrom = ConstsAccesEngin.AppEmail, List<string> ccList = null, string mailFromName = null, List<string> BCCList = null, List<string> files = null)
        {
            using (SmtpClient client = new SmtpClient())
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailFrom, ConstsAccesEngin.AppName);
                    mail.HeadersEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Priority = MailPriority.High;
                    mail.Subject = Subject;
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    #region foreach item in To?.Distinct()

                    foreach (var item in To?.Distinct())

                    {

                        mail.To.Add(item);

                    }

                    #endregion

                    #region ccList?.Count > 0

                    if (ccList?.Count > 0)

                    {

                        foreach (var ccEmail in ccList)

                        {

                            mail.CC.Add(ccEmail);

                        }

                    }

                    #endregion

                    #region BCCList?.Count > 0

                    if (BCCList?.Count > 0)

                    {

                        foreach (var bccEmail in BCCList)

                        {

                            mail.Bcc.Add(bccEmail);

                        }

                    }

                    #endregion

                    #region files?.Count > 0

                    if (files?.Count > 0)

                    {

                        foreach (var file in files)

                        {

                            mail.Attachments.Add(new Attachment(file));

                        }

                    }

                    #endregion
                    try
                    {
                        await client.SendMailAsync(mail);
                    }
                    catch (Exception ex)
                    {
                    }

                }

            }

        }
        public static async Task SendEmailDemandeEngin(List<string> To, string Body, string Subject, string mailFrom = ConstsAccesEngin.AppEmail, List<string> ccList = null, string mailFromName = null, List<string> BCCList = null, List<string> files = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                SmtpServer.Port = 587; 
                mail.From = new MailAddress(mailFrom, ConstsAccesEngin.AppName);
                mail.HeadersEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.Subject = Subject;
                mail.Body = Body;

                #region foreach item in To?.Distinct()

                foreach (var item in To?.Distinct())

                {

                    mail.To.Add(item);

                }

                #endregion

                #region ccList?.Count > 0

                if (ccList?.Count > 0)

                {

                    foreach (var ccEmail in ccList)

                    {

                        mail.CC.Add(ccEmail);

                    }

                }

                #endregion

                #region BCCList?.Count > 0

                if (BCCList?.Count > 0)

                {

                    foreach (var bccEmail in BCCList)

                    {

                        mail.Bcc.Add(bccEmail);

                    }

                }

                #endregion

                #region files?.Count > 0

                if (files?.Count > 0)

                {

                    foreach (var file in files)

                    {

                        mail.Attachments.Add(new Attachment(file));

                    }

                }

                #endregion
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("elmehdielmellali.mobile@gmail.com", "A1Z2E3R4");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }

        }


        public static async Task sendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                mail.From = new MailAddress("elmehdielmellali.mobile@gmail.com");
                mail.To.Add("elmehdielmellali.mobile@gmail.com");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("elmehdielmellali.mobile@gmail.com", "A1Z2E3R4");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
