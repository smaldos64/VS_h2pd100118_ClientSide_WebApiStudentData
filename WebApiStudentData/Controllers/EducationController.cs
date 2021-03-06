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

    public class EducationController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Uddannelsessteder. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle Uddannelsessteder med tilhørende Uddannelsessted ID.
        /// Listen returneres som en liste af jSon objekter, hvor hver enkelt jSon element 
        /// indeholder felterne : EducationID og EducationName.
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
        /// Returnerer info om ét specifikt Uddannelsessted ud fra id.
        /// </summary>
        /// <param name="id">Uddannelsessted ID</param>
        /// <returns>
        /// Ét Uddannelsessted. Uddannelsesstedet returneres som et jSon objekt, 
        /// som indeholder felterne : EducationID og EducationName.
        /// Eller et json Objekt med felterne ErrorNumber og ErrorText hvor ErrorNumber har en værdi 
        /// mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her
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
        /// data til funktionen med følgende felter specificeret : EducationName</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på det gemte Uddannelsessted. 
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
                if (null == json_Object.EducationName)
                {
                    return (Const.WrongjSonObjectParameters);
                }
                else
                {
                    if (Const.ObjectNotFound == Education.CheckForEducationNameInEducation((string)json_Object.EducationName))
                    {
                        Education_Object.EducationName = json_Object.EducationName;

                        db.Educations.Add(Education_Object);
                        NumberOfEducationsSaved = db.SaveChanges();

                        if (1 == NumberOfEducationsSaved)
                        {
                            LogData.LogDataToDatabase(UserName, DataBaseOperation.SaveData_Enum, ModelDatabaseNumber.Education_Enum);
                            return (Education_Object.EducationID);
                        }
                        else
                        {
                            return (Const.SaveOperationFailed);
                        }
                    }
                    else
                    {
                        return (Const.ObjectAlreadyPresent);
                    }
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Opdaterer ét Uddannelsessted udfra id. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : EducationName</param>
        /// <param name="id">Uddannelsessted ID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Uddannelsessted er opdateret ok. 
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
                if (null == json_Object.EducationName) 
                {
                    return (Const.WrongjSonObjectParameters);
                }
                else
                {
                    if (Const.ObjectNotFound == Education.CheckForEducationNameInEducation((string)json_Object.EducationName))
                    {
                        Education_Object = db.Educations.Find(id);

                        if (null != Education_Object)
                        {
                            Education_Object.EducationName = json_Object.EducationName;

                            NumberOfEducationsSaved = db.SaveChanges();
                            if (1 == NumberOfEducationsSaved)
                            {
                                LogData.LogDataToDatabase(UserName, DataBaseOperation.UpdateData_Enum, ModelDatabaseNumber.Education_Enum);
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
                        return (Const.ObjectAlreadyPresent);
                    }
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Sletter ét specifikt Uddannelsessted udfra id. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="id">Uddannelsessted ID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
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
                    if (0 == EducationLine.FindEducationLine_With_Specified_EducationID(id))
                    {
                        db.Educations.Remove(Education_Object);
                        NumberOfEducationsDeleted = db.SaveChanges();
                        if (1 == NumberOfEducationsDeleted)
                        {
                            LogData.LogDataToDatabase(UserName, DataBaseOperation.DeleteData_Enum, ModelDatabaseNumber.Education_Enum);
                            return (Const.DeleteOperationOk);
                        }
                        else
                        {
                            return (Const.DeleteOperationFailed);
                        }
                    }
                    else
                    {
                        return (Const.SpecifiedContentStillInUseInTablesBelow);
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