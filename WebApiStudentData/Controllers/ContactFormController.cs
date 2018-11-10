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

        // GET api/<controller>/5
        public object Get(int id, string UserName, string Password)
        {
            object jSon_Object = new object();
            ContactForm ContactForm_Object = new ContactForm();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                ContactForm_Object = db.ContactForms.Find(id);

                if (null != ContactForm_Object)
                {
                    if (UserID == ContactForm_Object.UserInfoID)
                    {
                        var ListItem = new
                        {
                            ContactFormID = ContactForm_Object.ContactFormID,
                            ContactNameFrom = ContactForm_Object.ContactNameFrom,
                            ContactNameEmail = ContactForm_Object.ContactNameEmail,
                            ContactText = ContactForm_Object.ContactText
                        };
                        jSon_Object = ListItem;
                    }
                    else
                    {
                        var ListItem = new
                        {
                            ErrorCode = Const.ObjectNotSavedByCurrentUserOriginally,
                            ErrorText = Const.FindReturnString(Const.ObjectNotSavedByCurrentUserOriginally)
                        };
                        jSon_Object = ListItem;
                    }
                }
                else
                {
                    var ListItem = new
                    {
                        ErrorCode = Const.ObjectNotFound,
                        ErrorText = Const.FindReturnString(Const.ObjectNotFound)
                    };
                    jSon_Object = ListItem;
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.UserNotFound,
                    ErrorText = Const.FindReturnString(Const.UserNotFound)
                };
                jSon_Object = ListItem;
            }
            return (jSon_Object);
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
                ContactForm_Object.UserInfoID = UserID;

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
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            ContactForm ContactForm_Object = new ContactForm();
            int NumberOfContactFormsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                ContactForm_Object = db.ContactForms.Find(id);
                if (null != ContactForm_Object)
                {
                    if (UserID == ContactForm_Object.UserInfoID)
                    {
                        ContactForm_Object.ContactNameFrom = json_Object.ContactNameFrom;
                        ContactForm_Object.ContactNameEmail = json_Object.ContactNameEmail;
                        ContactForm_Object.ContactText = json_Object.ContactText;

                        NumberOfContactFormsSaved = db.SaveChanges();
                        if (1 == NumberOfContactFormsSaved)
                        {
                            return (Const.UpdateOperationOk);
                        }
                        else
                        {
                            return (Const.UpdateOperationFailed);
                        }
                    }
                    else
                    {
                        return (Const.ObjectNotSavedByCurrentUserOriginally);
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