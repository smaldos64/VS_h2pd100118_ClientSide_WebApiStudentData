using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;
using System.Web.Http.Cors;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]

    public class ContactFormController : ApiController
    {
        // GET api/<controller>
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        public List<Object> Get(string UserName, string Password)
        {
            List<object> jSonList = new List<object>();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                List<ContactForm> ContactFormList = new List<ContactForm>();
                ContactFormList = db.ContactForms.Where(u => u.UserInfoID == UserID).ToList();

                foreach (ContactForm ContactForm_Object in ContactFormList)
                {
                    var ListItem = new
                    {
                        ContactFormID = ContactForm_Object.ContactFormID,
                        ContactNameFrom = ContactForm_Object.ContactNameFrom,
                        ContactNameEmail = ContactForm_Object.ContactNameEmail,
                        ContactText = ContactForm_Object.ContactText
                    };
                    jSonList.Add(ListItem);
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorNumber = Const.UserNotFound
                };
                jSonList.Add(ListItem);
            }

            return (jSonList);
        }

        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            ContactForm ContactForm_Object = new ContactForm();
            int NumberOfContactFormsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                ContactForm_Object.ContactNameFrom = json_Object.ContactNameFrom;
                ContactForm_Object.ContactNameEmail = json_Object.ContactNameEmail;
                ContactForm_Object.ContactText = json_Object.ContactText;

                db.ContactForms.Add(ContactForm_Object);
                NumberOfContactFormsSaved = db.SaveChanges();

                if (1 == NumberOfContactFormsSaved)
                {
                    return (ContactForm_Object.ContactFormID);
                }
                else
                {
                    return (Const.SaveOperationFailed);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            ContactForm ContactForm_Object = new ContactForm();
            int NumberOfContactFormsDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                ContactForm_Object = db.ContactForms.Find(id);
                if (null != ContactForm_Object)
                {
                    db.ContactForms.Remove(ContactForm_Object);
                    NumberOfContactFormsDeleted = db.SaveChanges();
                    if (1 == NumberOfContactFormsDeleted)
                    {
                        return (Const.DeleteOperationOk);
                    }
                    else
                    {
                        return (Const.DeleteOperationFailed);
                    }
                }
                else
                {
                    return (Const.ObjectNotFound);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }
    }
}