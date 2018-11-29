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

    public class EducationLineController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Uddannelseslinjer på alle Uddannelsessteder. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle Uddannelseslinjer på alle Uddannelsessteder med tilhørende 
        /// Uddannelsessted ID.
        /// </returns>
        /// <returns>
        /// Liste af alle Uddannelseslinjer på alle Uddannelsessteder. Listen returneres 
        /// som en liste af jSon objekter, hvor hver enkelt jSon element indeholder felterne : 
        /// EducationLineID, EducationLineName og EducationName.
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<EducationLine> EducationLine_List = new List<EducationLine>();

            EducationLine_List = db.EducationLines.ToList();

            foreach (EducationLine EducationLine_Object in EducationLine_List)
            {
                var ListItem = new
                {
                    EducationLineID = EducationLine_Object.EducationLineID,
                    EducationLineName = EducationLine_Object.EducationLineName,
                    EducationName = EducationLine_Object.Education.EducationName
                };

                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer info om specifikke Uddannelseslinjer på ét specifikt Uddannelsessted udfra Uddannelsessted ID
        /// </summary>
        /// <param name="id">Uddannelsessted ID</param>
        /// <returns>
        /// Returnerer Uddannelseslinje navn og Uddannelsessted navn udfra Uddannelseslinje ID
        /// </returns>
        /// /// <returns>
        /// Én liste af Uddannelseslinjer på ét Uddannelsessted. Listen returneres 
        /// som en liste af jSon objekter, hvor hver enkelt jSon element indeholder felterne : 
        /// EducationLineID, EducationLineName og EducationName.
        /// Eller et json Objekt med felterne ErrorNumber og ErrorText hvor ErrorNumber har en værdi 
        /// mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/<controller>/5
        public List<Object> Get(int id)
        {
            List<object> jSonList = new List<object>();
            List<EducationLine> EducationLine_List = new List<EducationLine>();

            EducationLine_List = db.EducationLines.Where(e => e.EducationID == id).ToList();

            if (EducationLine_List.Count > 0)
            {
                foreach (EducationLine EducationLine_Object in EducationLine_List)
                {
                    var ListItem = new
                    {
                        EducationLineID = EducationLine_Object.EducationLineID,
                        EducationLineName = EducationLine_Object.EducationLineName,
                        EducationName = EducationLine_Object.Education.EducationName
                    };

                    jSonList.Add(ListItem);
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.ObjectNotFound,
                    ErrorText = Const.FindReturnString(Const.ObjectNotFound)
                };
                jSonList.Add(ListItem);
            }

            return (jSonList);
        }

        /// <summary>
        /// Gemmer en ny Uddannelseslinje på et specificeret Uddannelssted. Ved kald af denne funktionalitet
        /// skal man angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : EducationLineName og EducationID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på den gemte Uddannelseslinje. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.EducationLineName) ||
                    (null == json_Object.EducationID)) 
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    EducationLine_Object.EducationLineName = json_Object.EducationLineName;
                    EducationLine_Object.EducationID = json_Object.EducationID;

                    db.EducationLines.Add(EducationLine_Object);
                    NumberOfEducationLinesSaved = db.SaveChanges();

                    if (1 == NumberOfEducationLinesSaved)
                    {
                        return (EducationLine_Object.EducationLineID);
                    }
                    else
                    {
                        return (Const.SaveOperationFailed);
                    }
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Opdaterer én Uddannelseslinje på ét specifikt Uddannelsessted udfra id. 
        /// Ved kald af denne funktionalitet skal man angive Brugernavn og Passsword. Kun brugere 
        /// kendt af systemet kan udnytte denne funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : EducationLineName og EducationID.</param>
        /// <param name="id">Uddannelseslinje ID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Uddannelseslinje er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.EducationLineName) ||
                    (null == json_Object.EducationID))
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    EducationLine_Object = db.EducationLines.Find(id);
                    if (null != EducationLine_Object)
                    {
                        EducationLine_Object.EducationLineName = json_Object.EducationName;
                        EducationLine_Object.EducationID = json_Object.EducationID;

                        NumberOfEducationLinesSaved = db.SaveChanges();
                        if (1 == NumberOfEducationLinesSaved)
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
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Sletter én specifik Uddannelseslinje på et specifikt Uddannelsessted udfra id. 
        /// Ved kald af denne funktionalitet skal man angive Brugernavn og Passsword. Kun brugere kendt 
        /// af systemet kan udnytte denne funktionalitet.
        /// </summary>
        /// <param name="id">Uddannelseslinje ID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Uddannelseslinje er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                EducationLine_Object = db.EducationLines.Find(id);
                if (null != EducationLine_Object)
                {
                    if (0 == User_Education_Time_Collection.FindEducationTime_Collection_With_Specified_EducationLineID(id))
                    {
                        db.EducationLines.Remove(EducationLine_Object);
                        NumberOfEducationLinesDeleted = db.SaveChanges();
                        if (1 == NumberOfEducationLinesDeleted)
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