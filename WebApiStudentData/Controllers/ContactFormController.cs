﻿using System;
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
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle kontaktformularer gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <returns>Liste af kontaktformularer. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
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

        /// <summary>
        /// Returnerer info om én kontaktformular udfra id gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <returns>Én kontaktformular. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
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

        /// <summary>
        /// Gemmer kontaktformular hørende til bruger specificeret ved UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige.
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : ContactNameFrom, ContactNameEmail 
        /// og ContactText.
        /// </param>
        /// <returns>
        /// Id nummeret på den gemte kontaktformular. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
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

        /// <summary>
        /// Ændrer kontaktformular hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// kontaktformular med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : ContactNameFrom, ContactNameEmail 
        /// og ContactText.
        /// </param>
        /// <param name="id">Integer der specificerer id på kontaktformular.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis kontaktformular er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
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

        /// <summary>
        /// Sletter kontaktformular hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// kontaktformular med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis kontaktformular er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
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