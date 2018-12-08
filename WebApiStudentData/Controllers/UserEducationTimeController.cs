using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;
using WebApiStudentData.ViewModels;
using System.Web.Http.Cors;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]

    public class UserEducationTimeController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Uddannelsesforløb gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Liste af Uddannelsesforløb. Listen returneres som en liste af jSon objekter, 
        /// hvor hver enkelt jSon element indeholder felterne : User_Education_Time_CollectionID,
        /// UserInfoID, UserName, EducationName, EducationLineName, 
        /// CharacterValueCourse, AbsencePercentageCourse, EducationLine, EducationName, 
        /// WhichCharacterScaleIDEducation, WhichCharacterScaleNameEducation, 
        /// CharacterValueEducation, EducationStartTime, EducationEndTime, CharacterValueEducation,  
        /// AbsencePercentageForEducation og CourseCharacterList. Feltet CourseCharacterList indeholder 
        /// en liste af Liste af Fag/Kursus forløb knyttet til de enkelte uddannelsesforløb. For en  
        /// beskrivelse af feltet CourseCharacterList henvises til UserEducationCourseCharacter eller klik 
        /// her : <see cref="UserEducationCourseCharacterController"/>.
        /// Flere af de nævnte felter "ID felter", kan have en værdi på -10 (InformationNotProvided), 
        /// hvis disse felter ikke er udfyldt af brugeren. Er det et tekst felt, vil feltet have værdien : 
        /// "Information er ikke gemt".
        /// Ved fejl vil der returneres et json Objekt med felterne ErrorNumber og ErrorText, 
        /// hvor ErrorNumber har en værdi mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her : <see cref="ReturnCodeAndReturnString"/>.
        /// </returns>
        // GET api/<controller>
        public List<Object> Get(string UserName, string Password)
        {
            List<object> jSonList = new List<object>();
            List<User_Education_Time_Collection> User_Education_Time_List = new List<User_Education_Time_Collection>();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (0 < UserID)
            {
                User_Education_Time_List = db.User_Education_Time_Collections.Where(u => u.UserInfoID == UserID).ToList();

                foreach (User_Education_Time_Collection User_Education_Time_Object in User_Education_Time_List)
                {
                    var ListItem = new
                    {
                        User_Education_Time_CollectionID = User_Education_Time_Object.User_Education_Time_CollectionID,
                        UserInfoID = User_Education_Time_Object.UserInfoID,
                        UserName = User_Education_Time_Object.UserInfo.UserName,
                        EducationName = User_Education_Time_Object.EducationLine.Education.EducationName,
                        EducationLineName = User_Education_Time_Object.EducationLine.EducationLineName,
                        WhichCharacterScaleIDEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided,
                        WhichCharacterScaleNameEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    Const.FindReturnString(Const.InformationNotProvided),
                        CharacterValueEducation = (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                  User_Education_Time_Object.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                        EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                        EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString(),
                        AbsencePercentageForEducation = (null != User_Education_Time_Object.AbsencePercentageEducation) ?
                                                        User_Education_Time_Object.AbsencePercentageEducation :
                                                        Const.InformationNotProvided,
                        CourseCharacterList = new List<VM_User_Education_Character_Course_Collection>()
                    };

                    foreach (User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object in
                        User_Education_Time_Object.User_Education_Character_Course_Collection)
                    {
                        VM_User_Education_Character_Course_Collection VM_User_Education_Character_Course_Collection_Object =
                            new VM_User_Education_Character_Course_Collection();
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object =
                            new User_Education_Character_Course_Collection();

                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID =
                          User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID;
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID =
                            User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID;
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.CourseID =
                            User_Education_Character_Course_Collection_Object.CourseID;
                        VM_User_Education_Character_Course_Collection_Object.CourseName = User_Education_Character_Course_Collection_Object.Course.CourseName;
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.WhichCharacterScaleID =
                            (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided;
                        VM_User_Education_Character_Course_Collection_Object.WhichCharacterScaleName = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                    User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    Const.FindReturnString(Const.InformationNotProvided);
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.CharacterValueCourse =
                            (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                  User_Education_Time_Object.CharacterValueEducation :
                                                  Const.InformationNotProvided;
                        VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.AbsencePercentageCourse =
                            (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                                                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                                                        Const.InformationNotProvided;

                        ListItem.CourseCharacterList.Add(VM_User_Education_Character_Course_Collection_Object);
                        //ListItem.CourseCharacterList.Add(User_Education_Character_Course_Collection_Object);
                        // Hvis man bruger linjen herover, som vil det være nemmeste, får man jSon self referencing fejl !!!

                    }
                    jSonList.Add(ListItem);
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
        /// Returnerer info om ét Uddannelsesforløb udfra id gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <param name="id">Integer der specificerer id på Bruger-Uddannnelsesforløb samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>Ét Uddannelsesforløb.Uddannelsesforløbet returneres som et jSon objekt, 
        /// som indeholder felterne : User_Education_Time_CollectionID,
        /// UserInfoID, UserName, EducationName, EducationLineName, 
        /// CharacterValueCourse, AbsencePercentageCourse, EducationLine, EducationName, 
        /// WhichCharacterScaleIDEducation, WhichCharacterScaleNameEducation, 
        /// CharacterValueEducation, EducationStartTime, EducationEndTime, CharacterValueEducation,  
        /// AbsencePercentageForEducation og CourseCharacterList. Feltet CourseCharacterList indeholder 
        /// en liste af Liste af Fag/Kursus forløb knyttet til de enkelte uddannelsesforløb. For en  
        /// beskrivelse af feltet CourseCharacterList henvises til UserEducationCourseCharacter eller klik 
        /// her : <see cref="UserEducationCourseCharacterController"/>.
        /// Flere af de nævnte felter "ID felter", kan have en værdi på -10 (InformationNotProvided), 
        /// hvis disse felter ikke er udfyldt af brugeren. Er det et tekst felt, vil feltet have værdien : 
        /// "Information er ikke gemt".
        /// Ved fejl vil der returneres et json Objekt med felterne ErrorNumber og ErrorText, 
        /// hvor ErrorNumber har en værdi mindre end 0. Se en oversigt over return koder i ReturnCodesAndStrings 
        /// eller klik her : <see cref="ReturnCodeAndReturnString"/>.
        /// </returns>
        // GET api/<controller>/5
        public object Get(int id, string UserName, string Password)
        {
            object jSon_Object = new object();
            User_Education_Time_Collection User_Education_Time_Object = new User_Education_Time_Collection();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (0 < UserID)
            {
                if (null != db.User_Education_Time_Collections.FirstOrDefault(u => u.User_Education_Time_CollectionID == id))
                {
                    if (null != db.User_Education_Time_Collections.FirstOrDefault(u => u.UserInfoID == UserID && u.User_Education_Time_CollectionID == id))
                    {
                        User_Education_Time_Object = db.User_Education_Time_Collections.First(u => u.UserInfoID == UserID && u.User_Education_Time_CollectionID == id);

                        var ListItem = new
                        {
                            User_Education_Time_CollectionID = User_Education_Time_Object.User_Education_Time_CollectionID,
                            UserInfoID = User_Education_Time_Object.UserInfoID,
                            UserName = User_Education_Time_Object.UserInfo.UserName,
                            EducationName = User_Education_Time_Object.EducationLine.Education.EducationName,
                            EducationLine = User_Education_Time_Object.EducationLine.EducationLineName,
                            WhichCharacterScaleIDEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided,
                            WhichCharacterScaleNameEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    Const.FindReturnString(Const.InformationNotProvided),
                            CharacterValueEducation = (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                  User_Education_Time_Object.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                            EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                            EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString(),
                            AbsencePercentageForEducation = (null != User_Education_Time_Object.AbsencePercentageEducation) ?
                                                        User_Education_Time_Object.AbsencePercentageEducation :
                                                        Const.InformationNotProvided,
                            CourseCharacterList = new List<VM_User_Education_Character_Course_Collection>()
                        };

                        foreach (User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object in
                            User_Education_Time_Object.User_Education_Character_Course_Collection)
                        {
                            VM_User_Education_Character_Course_Collection VM_User_Education_Character_Course_Collection_Object =
                                new VM_User_Education_Character_Course_Collection();
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object =
                                new User_Education_Character_Course_Collection();

                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID =
                              User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID;
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID =
                                User_Education_Character_Course_Collection_Object.User_Education_Time_CollectionID;
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.CourseID =
                                User_Education_Character_Course_Collection_Object.CourseID;
                            VM_User_Education_Character_Course_Collection_Object.CourseName = User_Education_Character_Course_Collection_Object.Course.CourseName;
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.WhichCharacterScaleID =
                                (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                        User_Education_Character_Course_Collection_Object.WhichCharacterScaleID :
                                                        Const.InformationNotProvided;
                            VM_User_Education_Character_Course_Collection_Object.WhichCharacterScaleName = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                                                        User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                        "Ikke Oplyst !!!";
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.CharacterValueCourse =
                                (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                      User_Education_Time_Object.CharacterValueEducation :
                                                      Const.InformationNotProvided;
                            VM_User_Education_Character_Course_Collection_Object.User_Education_Character_Course_Collection_Object.AbsencePercentageCourse =
                                (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                                                            User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                                                            Const.InformationNotProvided;

                            ListItem.CourseCharacterList.Add(VM_User_Education_Character_Course_Collection_Object);
                        }
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
        /// Gemmer Uddannelsesforløb hørende til bruger specificeret ved UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige.
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : StartDate, EndDate, EducationLineID.
        /// Optionalt kan følgende felter også angives : WhichCharacterScaleID, CharacterValueEducation og  
        /// AbsencePercentageEducation
        /// </param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på det gemte Uddannelsesforløb. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            User_Education_Time_Collection User_Education_Time_Collection_Object = 
                new User_Education_Time_Collection();
            WhichCharacterScale WhichCharacterScale_Object = new WhichCharacterScale();
            int NumberOfUserEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.StartDate) ||
                    (null == json_Object.EndDate) ||
                    (null == json_Object.EducationLineID))
                {
                    return (Const.WrongjSonObjectParameters);
                }
                else
                {
                    User_Education_Time_Collection_Object.UserInfoID = UserID;
                    User_Education_Time_Collection_Object.StartDate = json_Object.StartDate;
                    User_Education_Time_Collection_Object.EndDate = json_Object.EndDate;
                    User_Education_Time_Collection_Object.EducationLineID = json_Object.EducationLineID;

                    List<User_Education_Time_Collection> User_Education_Time_Present_Collection_List = db.User_Education_Time_Collections.Where(u => u.UserInfoID == UserID && u.EducationLineID == User_Education_Time_Collection_Object.EducationLineID).ToList();

                    int ListCounter = 0;

                    for (ListCounter = 0; ListCounter < User_Education_Time_Present_Collection_List.Count; ListCounter++)
                    {
                        if ((User_Education_Time_Collection_Object.StartDate == 
                            User_Education_Time_Present_Collection_List[ListCounter].StartDate) &&
                            (User_Education_Time_Collection_Object.EndDate ==
                            User_Education_Time_Present_Collection_List[ListCounter].EndDate))
                        {
                            return (Const.UserAlreadySignedUpForEducation);
                        }
                    }

                    if (null != json_Object.WhichCharacterScaleID)
                    {
                        WhichCharacterScale_Object = db.WhichCharacterScales.Find((int)json_Object.WhichCharacterScaleID);
                        if (null == WhichCharacterScale_Object)
                        {
                            return (Const.WrongCharacterScaleProvided);
                        }
                        User_Education_Time_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                    }
                    else
                    {
                        User_Education_Time_Collection_Object.WhichCharacterScaleID = null;
                    }

                    if (null != json_Object.CharacterValueEducation)
                    {
                        if (null == json_Object.WhichCharacterScaleID)
                        {
                            return (Const.CharacterProvidedButNoCharacterScaleProvided);
                        }
                        else
                        {
                            int WhichCharacterScale = WhichCharacterScale_Object.WhichCharacterScaleID;
                            int CharacterValueEducation = json_Object.CharacterValueEducation;

                            switch (WhichCharacterScale)
                            {
                                case (int)WhichCharacterScaleENUM.Character_7_ENUM:
                                    Character7Scale Character7Scale_Object = db.Character7Scales.FirstOrDefault(c => c.Character7ScaleValue == CharacterValueEducation);
                                    if (null == Character7Scale_Object)
                                    {
                                        return (Const.WrongCharacterProvided);
                                    }
                                    break;

                                case (int)WhichCharacterScaleENUM.Character_13_ENUM:
                                    Character13Scale Character13Scale_Object = db.Character13Scales.FirstOrDefault(c => c.Character13ScaleValue == CharacterValueEducation);
                                    if (null == Character13Scale_Object)
                                    {
                                        return (Const.WrongCharacterProvided);
                                    }
                                    break;
                            }
                        }
                        User_Education_Time_Collection_Object.CharacterValueEducation = json_Object.CharacterValueEducation;
                    }
                    else
                    {
                        if (null != json_Object.WhichCharacterScaleID)
                        {
                            return (Const.NoCharacterProvidedButCharacterScaleProvided);
                        }
                        User_Education_Time_Collection_Object.CharacterValueEducation = null;
                    }

                    if (null != json_Object.AbsencePercentageEducation)
                    {
                        User_Education_Time_Collection_Object.AbsencePercentageEducation = json_Object.AbsencePercentageEducation;
                    }
                    else
                    {
                        User_Education_Time_Collection_Object.AbsencePercentageEducation = null;
                    }

                    db.User_Education_Time_Collections.Add(User_Education_Time_Collection_Object);
                    NumberOfUserEducationsSaved = db.SaveChanges();

                    if (1 == NumberOfUserEducationsSaved)
                    {
                        return (User_Education_Time_Collection_Object.User_Education_Time_CollectionID);
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
        /// Opdaterer Uddannelsesforløb hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// Uddannelsesforløb med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : StartDate, EndDate, EducationLineID.
        /// Optionalt kan følgende felter også angives : WhichCharacterScaleID, CharacterValueEducation og  
        /// AbsencePercentageEducation
        /// </param>
        /// <param name="id">Integer der specificerer id på Bruger-Uddannnelsesforløb samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Uddannelsesforløb er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            User_Education_Time_Collection User_Education_Time_Collection_Object =
                new User_Education_Time_Collection();
            WhichCharacterScale WhichCharacterScale_Object = new WhichCharacterScale();
            int NumberOfUserEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.StartDate) ||
                    (null == json_Object.EndDate) ||
                    (null == json_Object.EducationLineID))
                {
                    return (Const.WrongjSonObjectParameters);
                }
                else
                {
                    User_Education_Time_Collection_Object = db.User_Education_Time_Collections.Find(id);

                    if (null != User_Education_Time_Collection_Object)
                    {
                        User_Education_Time_Collection_Object.StartDate = json_Object.StartDate;
                        User_Education_Time_Collection_Object.EndDate = json_Object.EndDate;
                        User_Education_Time_Collection_Object.EducationLineID = json_Object.EducationLineID;

                        List<User_Education_Time_Collection> User_Education_Time_Present_Collection_List = db.User_Education_Time_Collections.Where(u => u.UserInfoID == UserID && u.EducationLineID == User_Education_Time_Collection_Object.EducationLineID).ToList();

                        int ListCounter = 0;

                        for (ListCounter = 0; ListCounter < User_Education_Time_Present_Collection_List.Count; ListCounter++)
                        {
                            if (id != User_Education_Time_Present_Collection_List[ListCounter].User_Education_Time_CollectionID)
                            {
                                if ((User_Education_Time_Collection_Object.StartDate ==
                                    User_Education_Time_Present_Collection_List[ListCounter].StartDate) &&
                                    (User_Education_Time_Collection_Object.EndDate ==
                                    User_Education_Time_Present_Collection_List[ListCounter].EndDate))
                                {
                                    return (Const.UserAlreadySignedUpForEducation);
                                }
                            }
                        }

                        if (null != json_Object.WhichCharacterScaleID)
                        {
                            WhichCharacterScale_Object = db.WhichCharacterScales.Find((int)json_Object.WhichCharacterScaleID);
                            if (null == WhichCharacterScale_Object)
                            {
                                return (Const.WrongCharacterScaleProvided);
                            }
                            User_Education_Time_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                        }
                        else
                        {
                            User_Education_Time_Collection_Object.WhichCharacterScaleID = null;
                        }

                        if (null != json_Object.CharacterValueEducation)
                        {
                            if (null == json_Object.WhichCharacterScaleID)
                            {
                                return (Const.CharacterProvidedButNoCharacterScaleProvided);
                            }
                            else
                            {
                                int WhichCharacterScale = WhichCharacterScale_Object.WhichCharacterScaleID;
                                int CharacterValueEducation = json_Object.CharacterValueEducation;

                                switch (WhichCharacterScale)
                                {
                                    case (int)WhichCharacterScaleENUM.Character_7_ENUM:
                                        Character7Scale Character7Scale_Object = db.Character7Scales.FirstOrDefault(c => c.Character7ScaleValue == CharacterValueEducation);
                                        if (null == Character7Scale_Object)
                                        {
                                            return (Const.WrongCharacterProvided);
                                        }
                                        break;

                                    case (int)WhichCharacterScaleENUM.Character_13_ENUM:
                                        Character13Scale Character13Scale_Object = db.Character13Scales.FirstOrDefault(c => c.Character13ScaleValue == CharacterValueEducation);
                                        if (null == Character13Scale_Object)
                                        {
                                            return (Const.WrongCharacterProvided);
                                        }
                                        break;
                                }
                            }
                            User_Education_Time_Collection_Object.CharacterValueEducation = json_Object.CharacterValueEducation;
                        }
                        else
                        {
                            if (null != json_Object.WhichCharacterScaleID)
                            {
                                return (Const.NoCharacterProvidedButCharacterScaleProvided);
                            }
                            User_Education_Time_Collection_Object.CharacterValueEducation = null;
                        }

                        if (null != json_Object.AbsencePercentageEducation)
                        {
                            User_Education_Time_Collection_Object.AbsencePercentageEducation = json_Object.AbsencePercentageEducation;
                        }
                        else
                        {
                            User_Education_Time_Collection_Object.AbsencePercentageEducation = null;
                        }

                        NumberOfUserEducationsSaved = db.SaveChanges();
                        if (1 == NumberOfUserEducationsSaved)
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
        /// Sletter Uddannelsesforløb hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// Uddannelsesforløb med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="id">Integer der specificerer id på Bruger-Uddannnelsesforløb samling.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Uddannelsesforløb er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            User_Education_Time_Collection User_Education_Time_Collection_Object =
                new User_Education_Time_Collection();
            int NumberOfUserEducationsDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                User_Education_Time_Collection_Object = db.User_Education_Time_Collections.Find(id);
                if (null != User_Education_Time_Collection_Object)
                {
                    db.User_Education_Time_Collections.Remove(User_Education_Time_Collection_Object);
                    NumberOfUserEducationsDeleted = db.SaveChanges();
                    if (1 == NumberOfUserEducationsDeleted)
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