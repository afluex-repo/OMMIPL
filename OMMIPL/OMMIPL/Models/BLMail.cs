using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BusinessLayer
{
    public class BLMail
    {
        public static void SendNFCActivationMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("activation@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("activation@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendPasswordMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("noreply@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("noreply@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendVerificationMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("verification@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("verification@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendPurchaseMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("purchase@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("purchase@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendDistributorMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("distributor@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("distributor@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendOffersMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("offers@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("offers@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }
        public static void SendTransactionMail(string UserName, string MailBody, string Subject, string MailId)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            string str = string.Empty;
            string MailText = string.Empty;
            message.From = new MailAddress("transaction@urdost.com");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/index.html")))
            {
                MailText = reader.ReadToEnd();
            }
            MailText = MailText.Replace("[UserName]", UserName);
            MailText = MailText.Replace("[MailBody]", MailBody);
            message.Subject = Subject;
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = MailText;
            smtp.Port = 587;
            smtp.Host = "smtp.hostinger.com"; //for gmail host  
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("transaction@urdost.com", "Afluex@123");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            message.To.Add(new MailAddress(MailId));
            smtp.Send(message);
        }

    }
}
