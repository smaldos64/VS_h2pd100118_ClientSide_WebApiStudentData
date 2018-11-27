using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace WebApiStudentData.Tools
{
    public class Mail
    {
        public static void SendMail(string MailMessageFrom, string MailMesageTo, string MailSubject,
                                    string MailBody)
        {
            string ErrorString;

            MailMessage MailMessage_Object = new MailMessage();
            SmtpClient SmtpClient_Object = new SmtpClient("smtp.gmail.com");

            MailMessage_Object.From = new MailAddress(MailMessageFrom);
            MailMessage_Object.To.Add(MailMesageTo);
            MailMessage_Object.Subject = MailSubject;
            MailMessage_Object.Body = MailBody;

            SmtpClient_Object.Port = 587;
            SmtpClient_Object.Credentials = new System.Net.NetworkCredential("technologycollege2018@gmail.com", "Technologycollege_2018");

            SmtpClient_Object.EnableSsl = true;

            try
            {
                SmtpClient_Object.Send(MailMessage_Object);
            }
            catch (Exception Error)
            {
                ErrorString = Error.ToString();
            }
        }
    }
}