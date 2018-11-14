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

    public class EducationController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Uddannelsessteder. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle Uddannelsessteder med tilhørende Uddannelsessted ID.
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<Education> Education_List = new List<Education>();

            Education_List = db.Educations.ToList();

            foreach (Education Education_Object in Education_List)
            {
                var ListItem = new
                {
                    EducationID = Education_Object.EducationID,
                    EducationName = Education_Object.EducationName
                };

                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer info om ét specifikt Uddannelsessted udfra ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Returnerer Uddannelsessted navn udfra Uddannelsessted ID
        /// </returns>
        // GET api/<controller>/5
        public Object Get(int id)
        {
            object jSon_Object = new object();
            Education Education_Object  = new Education();

            Education_Object = db.Educations.Find(id);

            if (null != Education_Object)
            {
                var ListItem = new
                {
                    EducationID = Education_Object.EducationID,
                    EducationName = Education_Object.EducationName
                };
                jSon_Object = ListItem;
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
           
            return (jSon_Object);
        }

        /// <summary>
        /// Gemmer et nyt Uddannelsessted. Ved kald af denne funktionalitet skal man angive
        /// Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : EducationName
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>
        /// Id nummeret på det gemte Fag/Kursus. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            Education Education_Object = new Education();
            int NumberOfEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                Education_Object.EducationName = json_Object.EducationName;

                db.Educations.Add(Education_Object);
                NumberOfEducationsSaved = db.SaveChanges();

                if (1 == NumberOfEducationsSaved)
                {
                    return (Education_Object.EducationID);
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
        /// Opdaterer ét Uddannelsessted udfra Uddannelsessted ID. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : EducationName
        /// <param name="id"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Kursus/Fag er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            Education Education_Object = new Education();
            int NumberOfEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                Education_Object = db.Educations.Find(id);

                if (null != Education_Object)
                {
                    Education_Object.EducationName = json_Object.EducationName;

                    NumberOfEducationsSaved = db.SaveChanges();
                    if (1 == NumberOfEducationsSaved)
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
                    return (Const.ObjectNotFound);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Sletter ét specifikt Uddannelsessted udfra Uddannelsessted ID. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Fag/Kursus er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            Education Education_Object = new Education();
            int NumberOfEducationsDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                Education_Object = db.Educations.Find(id);
                if (null != Education_Object)
                {
                    db.Educations.Remove(Education_Object);
                    NumberOfEducationsDeleted = db.SaveChanges();
                    if (1 == NumberOfEducationsDeleted)
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