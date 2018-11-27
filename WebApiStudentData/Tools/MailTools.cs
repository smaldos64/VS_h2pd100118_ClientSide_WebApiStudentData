using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Tools
{
    public class MailTools
    {
        public static string PackMail(string MailSubject, string MailBody, string EmailAdressFrom, 
                                      string PhoneNumberFrom, int ID = 0)
        {
            string MailContent;
            
            if (null != MailSubject)
            {
                MailContent = "Emne               : " + MailSubject;
            }
            else
            {
                MailContent = "Emne er ikke angivet ";
            }
            
            MailContent += System.Environment.NewLine;
            MailContent += System.Environment.NewLine;
            MailContent += MailBody;
            MailContent += System.Environment.NewLine;
            MailContent += System.Environment.NewLine;
            MailContent += "EMail adresse (afsender) : " +EmailAdressFrom;
            MailContent += System.Environment.NewLine;
            MailContent += System.Environment.NewLine;
            MailContent += "Tlf. Nummer (afsender)   : " + PhoneNumberFrom;

            if (0 != ID)
            {
                MailContent += System.Environment.NewLine;
                MailContent += System.Environment.NewLine;
                MailContent += "Dette er en ændring af Kontakt Formular med ID   : " + ID.ToString();
            }

            return (MailContent);
        }
    }
}