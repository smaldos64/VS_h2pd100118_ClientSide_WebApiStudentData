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

    public class UserEducationCourseCharacterController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Fag/Kursus forløb på alle Uddannelsesforløb gemt af en bruger specificeret 
        /// ved UserName og Password.
        /// </summary>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Liste af Fag/Kursus forløb. Listen returneres som en liste af jSon objekter, 
        /// hvor hver enkelt jSon element indeholder felterne : User_Education_Character_Course_CollectionID,
        /// CourseID, CourseName, WhichCharacterScaleIDCourse, WhichCharacterScaleNameCourse, 
        /// CharacterValueCourse, AbsencePercentageCourse, EducationLine, EducationName, 
        /// EducationStartTime, EducationEndTime, CharacterValueEducation og 
        /// AbsencePercentageForEducation. Flere af de nævnte felter "ID felter", kan have 
        /// en værdi på -10 (InformationNotProvided), hvis disse felter ikke er udfyldt af
        /// brugeren. Er det et tekst felt, vil feltet have værdien : "Information er ikke gemt"
        /// Ved fejl vil der returneres et json Objekt med felterne ErrorNumber og ErrorText, 
        /// hvor ErrorNumber har en værdi mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/<controller>
        public List<Object> Get(string UserName, string Password)
        {
            List<object> jSonList = new List<object>();
            List<User_Education_Character_Course_Collection> User_Education_Character_Course_Collection_List = 
                new List<User_Education_Character_Course_Collection>();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (0 < UserID)
            {
                User_Education_Character_Course_Collection_List = db.User_Education_Character_Course_Collections.Where(u => u.User_Education_Time_Collection.UserInfoID == UserID).ToList();

                foreach (User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object in User_Education_Character_Course_Collection_List)
                {
                    var ListItemCourseCharacter = new
                    {
                        User_Education_Character_Course_CollectionID =
                                User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID,
                        CourseID = User_Education_Character_Course_Collection_Object.CourseID,
                        CourseName = User_Education_Character_Course_Collection_Object.Course.CourseName,
                        WhichCharacterScaleIDCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided,
                        WhichCharacterScaleNameCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    Const.FindReturnString(Const.InformationNotProvided),
                        CharacterValueCourse = (null != User_Education_Character_Course_Collection_Object.CharacterValueCourse) ?
                                                  User_Education_Character_Course_Collection_Object.CharacterValueCourse :
                                                  Const.InformationNotProvided,
                        AbsencePercentageCourse = (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                                                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                                                        Const.InformationNotProvided,
                        EducationLine = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EducationLine.EducationLineName,
                        EducationName = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EducationLine.Education.EducationName,
                        EducationStartTime = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.StartDate,
                        EducationEndTime = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EndDate,
                        CharacterValueEducation = (null != User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.CharacterValueEducation) ?
                                                  User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                        AbsencePercentageForEducation = (null != User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.AbsencePercentageEducation) ?
                                                        User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.AbsencePercentageEducation :
                                                        Const.InformationNotProvided
                    };

                    jSonList.Add(ListItemCourseCharacter);
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.UserNotFound,
                    ErrorText = Const.FindReturnString(Const.UserNotFound)
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer info om ét Fag/Kursus på ét specifikt Uddannelsesforløb udfra ID gemt af en bruger 
        /// specificeret ved UserName og Password.
        /// </summary>
        /// <param name="id">Integer der specificerer id på Bruger-Fag/Kursus samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>Ét Fag/Kursus forløb. Fag/Kursus forløbet returneres som et jSon objekt, 
        /// som indeholder felterne : User_Education_Character_Course_CollectionID,
        /// CourseID, CourseName, WhichCharacterScaleIDCourse, WhichCharacterScaleNameCourse, 
        /// CharacterValueCourse, AbsencePercentageCourse, EducationLine, EducationName, 
        /// EducationStartTime, EducationEndTime, CharacterValueEducation og 
        /// AbsencePercentageForEducation. Flere af de nævnte felter "ID felter", kan have 
        /// en værdi på -10 (InformationNotProvided), hvis disse felter ikke er udfyldt af
        /// brugeren. Er det et tekst felt, vil feltet have værdien : "Information er ikke gemt"
        /// Ved fejl vil der returneres et json Objekt med felterne ErrorNumber og ErrorText, 
        /// hvor ErrorNumber har en værdi mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/<controller>/5
        public object Get(int id, string UserName, string Password)
        {
            object jSon_Object = new object();
            User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object = 
                new User_Education_Character_Course_Collection();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                User_Education_Character_Course_Collection_Object = db.User_Education_Character_Course_Collections.Find(id);

                if (null != User_Education_Character_Course_Collection_Object)
                {
                    if (UserID == User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.UserInfoID)
                    {
                        var ListItemCourseCharacter = new
                        {
                            User_Education_Character_Course_CollectionID =
                                User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID,
                            CourseID = User_Education_Character_Course_Collection_Object.CourseID,
                            CourseName = User_Education_Character_Course_Collection_Object.Course.CourseName,
                            WhichCharacterScaleIDCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided,
                            WhichCharacterScaleNameCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    Const.FindReturnString(Const.InformationNotProvided),
                            CharacterValueCourse = (null != User_Education_Character_Course_Collection_Object.CharacterValueCourse) ?
                                                  User_Education_Character_Course_Collection_Object.CharacterValueCourse :
                                                  Const.InformationNotProvided,
                            AbsencePercentageCourse = (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                                                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                                                        Const.InformationNotProvided,
                            EducationLine = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EducationLine.EducationLineName,
                            EducationName = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EducationLine.Education.EducationName,
                            EducationStartTime = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.StartDate,
                            EducationEndTime = User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.EndDate,
                            CharacterValueEducation = (null != User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.CharacterValueEducation) ?
                                                  User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                            AbsencePercentageForEducation = (null != User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.AbsencePercentageEducation) ?
                                                        User_Education_Character_Course_Collection_Object.User_Education_Time_Collection.AbsencePercentageEducation :
                                                        Const.InformationNotProvided
                        };
                        jSon_Object = ListItemCourseCharacter;
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
        /// Gemmer Fag/Kursus forløb hørende til ét uddannelsesforløb specificeret ved UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige.
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : User_Education_Time_CollectionID, 
        /// CourseID.
        /// Optionalt kan følgende felter også angives : WhichCharacterScaleID, CharacterValueCourse og  
        /// AbsencePercentageCourse
        /// </param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på det gemte Fag/Kursus forløb. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object =
                new User_Education_Character_Course_Collection();
            WhichCharacterScale WhichCharacterScale_Object = new WhichCharacterScale();
            int NumberOfCourseCharactersSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.User_Education_Time_CollectionID) ||
                    (null == json_Object.CourseID))
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID =
                        json_Object.User_Education_Time_CollectionID;
                    User_Education_Character_Course_Collection_Object.CourseID =
                        json_Object.CourseID;

                    if (null != json_Object.WhichCharacterScaleID)
                    {
                        WhichCharacterScale_Object = db.WhichCharacterScales.Find((int)json_Object.WhichCharacterScaleID);
                        if (null == WhichCharacterScale_Object)
                        {
                            return (Const.WrongCharacterScaleProvided);
                        }
                        User_Education_Character_Course_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                    }
                    else
                    {
                        User_Education_Character_Course_Collection_Object.WhichCharacterScaleID = null;
                    }

                    if (null != json_Object.CharacterValueCourse)
                    {
                        if (null == json_Object.WhichCharacterScaleID)
                        {
                            return (Const.CharacterProvidedButNoCharacterScaleProvided);
                        }
                        else
                        {
                            int WhichCharacterScale = WhichCharacterScale_Object.WhichCharacterScaleID;

                            switch (WhichCharacterScale)
                            {
                                case (int)WhichCharacterScaleENUM.Character_7_ENUM:
                                    Character7Scale Character7Scale_Object = db.Character7Scales.Find((int)json_Object.CharacterValueCourse);
                                    if (null == Character7Scale_Object)
                                    {
                                        return (Const.WrongCharacterProvided);
                                    }
                                    break;

                                case (int)WhichCharacterScaleENUM.Character_13_ENUM:
                                    Character13Scale Character13Scale_Object = db.Character13Scales.Find((int)json_Object.CharacterValueCourse);
                                    if (null == Character13Scale_Object)
                                    {
                                        return (Const.WrongCharacterProvided);
                                    }
                                    break;
                            }
                        }
                        User_Education_Character_Course_Collection_Object.CharacterValueCourse = json_Object.CharacterValueCourse;
                    }
                    else
                    {
                        if (null != json_Object.WhichCharacterScaleID)
                        {
                            return (Const.NoCharacterProvidedButCharacterScaleProvided);
                        }
                        User_Education_Character_Course_Collection_Object.CharacterValueCourse = null;
                    }

                    if (null != json_Object.AbsencePercentageCourse)
                    {
                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse = json_Object.AbsencePercentageEducation;
                    }
                    else
                    {
                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse = null;
                    }

                    db.User_Education_Character_Course_Collections.Add(User_Education_Character_Course_Collection_Object);
                    NumberOfCourseCharactersSaved = db.SaveChanges();

                    if (1 == NumberOfCourseCharactersSaved)
                    {
                        return (User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID);
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
        /// Opdaterer Fag/Kursus forløb hørende til ét uddannelsesforløb specificeret ved id, UserName og 
        /// Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige.
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : User_Education_Time_CollectionID,
        /// CourseID.
        /// Optionalt kan følgende felter også angives : WhichCharacterScaleID, CharacterValueCourse og  
        /// AbsencePercentageCourse
        /// </param>
        /// <param name="id">Integer der specificerer id på Bruger-Fag/Kursus samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Fag/Kursus forløb er gemt ok.
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object =
                new User_Education_Character_Course_Collection();
            WhichCharacterScale WhichCharacterScale_Object = new WhichCharacterScale();
            int NumberOfCourseCharactersSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.User_Education_Time_CollectionID) ||
                    (null == json_Object.CourseID))
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    User_Education_Character_Course_Collection_Object = db.User_Education_Character_Course_Collections.Find(id);

                    if (null != User_Education_Character_Course_Collection_Object)
                    {
                        User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID =
                        json_Object.User_Education_Time_CollectionID;
                        User_Education_Character_Course_Collection_Object.CourseID =
                            json_Object.CourseID;

                        if (null != json_Object.WhichCharacterScaleID)
                        {
                            WhichCharacterScale_Object = db.WhichCharacterScales.Find((int)json_Object.WhichCharacterScaleID);
                            if (null == WhichCharacterScale_Object)
                            {
                                return (Const.WrongCharacterScaleProvided);
                            }
                            User_Education_Character_Course_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                        }
                        else
                        {
                            User_Education_Character_Course_Collection_Object.WhichCharacterScaleID = null;
                        }

                        if (null != json_Object.CharacterValueCourse)
                        {
                            if (null == json_Object.WhichCharacterScaleID)
                            {
                                return (Const.CharacterProvidedButNoCharacterScaleProvided);
                            }
                            else
                            {
                                int WhichCharacterScale = WhichCharacterScale_Object.WhichCharacterScaleID;

                                switch (WhichCharacterScale)
                                {
                                    case (int)WhichCharacterScaleENUM.Character_7_ENUM:
                                        Character7Scale Character7Scale_Object = db.Character7Scales.Find((int)json_Object.CharacterValueCourse);
                                        if (null == Character7Scale_Object)
                                        {
                                            return (Const.WrongCharacterProvided);
                                        }
                                        break;

                                    case (int)WhichCharacterScaleENUM.Character_13_ENUM:
                                        Character13Scale Character13Scale_Object = db.Character13Scales.Find((int)json_Object.CharacterValueCourse);
                                        if (null == Character13Scale_Object)
                                        {
                                            return (Const.WrongCharacterProvided);
                                        }
                                        break;
                                }
                            }
                            User_Education_Character_Course_Collection_Object.CharacterValueCourse = json_Object.CharacterValueCourse;
                        }
                        else
                        {
                            if (null != json_Object.WhichCharacterScaleID)
                            {
                                return (Const.NoCharacterProvidedButCharacterScaleProvided);
                            }
                            User_Education_Character_Course_Collection_Object.CharacterValueCourse = null;
                        }

                        if (null != json_Object.AbsencePercentageCourse)
                        {
                            User_Education_Character_Course_Collection_Object.AbsencePercentageCourse = json_Object.AbsencePercentageEducation;
                        }
                        else
                        {
                            User_Education_Character_Course_Collection_Object.AbsencePercentageCourse = null;
                        }

                        NumberOfCourseCharactersSaved = db.SaveChanges();
                        if (1 == NumberOfCourseCharactersSaved)
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
        /// Sletter Fag/Kursus forløb hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// Uddannelsesforløb med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="id">Integer der specificerer id på Bruger-Fag/Kursus samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Fag/Kursus forløb er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object =
                new User_Education_Character_Course_Collection();
            int NumberOfCourseCharactersDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                User_Education_Character_Course_Collection_Object = db.User_Education_Character_Course_Collections.Find(id);
                if (null != User_Education_Character_Course_Collection_Object)
                {
                    db.User_Education_Character_Course_Collections.Remove(User_Education_Character_Course_Collection_Object);
                    NumberOfCourseCharactersDeleted = db.SaveChanges();
                    if (1 == NumberOfCourseCharactersDeleted)
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