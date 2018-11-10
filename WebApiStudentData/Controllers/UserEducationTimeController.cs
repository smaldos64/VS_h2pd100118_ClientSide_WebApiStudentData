using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Controllers
{
    public class UserEducationTimeController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

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
                        EducationID = User_Education_Time_Object.User_Education_Time_CollectionID,
                        UserInfoID = User_Education_Time_Object.UserInfoID,
                        UserName = User_Education_Time_Object.UserInfo.UserName,
                        EducationPlace = User_Education_Time_Object.EducationLine.Education.EducationName,
                        EducationLine = User_Education_Time_Object.EducationLine.EducationLineName,
                        WhichCharacterScaleIDEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScaleID :
                                                    Const.InformationNotProvided,
                        WhichCharacterScaleNameEducation = (null != User_Education_Time_Object.WhichCharacterScaleID) ?
                                                    User_Education_Time_Object.WhichCharacterScale.WhichCharacterScaleName :
                                                    "Ikke Oplyst !!!",
                        CharacterValueEducation = (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                  User_Education_Time_Object.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                        EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                        EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString(),
                        AbsencePercentageForEducation = (null != User_Education_Time_Object.AbsencePercentageEducation) ?
                                                        User_Education_Time_Object.AbsencePercentageEducation :
                                                        Const.InformationNotProvided,
                        CourseCharacterList = new List<User_Education_Character_Course_Collection>
                    };

                    foreach (User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object in
                        User_Education_Time_Object.User_Education_Character_Course_Collection)
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
                                                    "Ikke Oplyst !!!",
                            CharacterValueCourse = (null != User_Education_Time_Object.CharacterValueEducation) ?
                                                  User_Education_Time_Object.CharacterValueEducation :
                                                  Const.InformationNotProvided,
                            AbsencePercentageCourse = (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                                                        User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                                                        Const.InformationNotProvided,
                            
                        };
                        ListItem.CourseCharacterList.Add(ListItemCourseCharacter);
                        
                    }

                    jSonList.Add(ListItem);
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.UserNotFound,
                    ErrorText = "Bruger er ikke fundet !!!"
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }


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

                        string CourseNameOut = "";
                        string WhichCharacterScaleOut = "";
                        int CharacterValueOut = -100;

                        User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object =
                            db.User_Education_Character_Course_Collections.FirstOrDefault(u => u.User_Education_Time_CollectionID == User_Education_Time_Object.User_Education_Time_CollectionID);
                        if (null != User_Education_Character_Course_Collection_Object)
                        {
                            CourseNameOut = User_Education_Character_Course_Collection_Object.Course.CourseName;
                            WhichCharacterScaleOut = User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName;
                            CharacterValueOut = User_Education_Character_Course_Collection_Object.CharacterValue;
                        }

                        string AbsencePercentageOut = "Ikke Oplyst";
                        Absence Absence_Object = db.Absences.FirstOrDefault(u => u.User_Education_Time_CollectionID == User_Education_Time_Object.User_Education_Time_CollectionID);
                        if (null != Absence_Object)
                        {
                            AbsencePercentageOut = Absence_Object.AbsencePercentage.ToString();
                        }
                        var ListItem = new
                        {
                            EducationID = User_Education_Time_Object.User_Education_Time_CollectionID,
                            UserInfoID = User_Education_Time_Object.UserInfoID,
                            UserName = User_Education_Time_Object.UserInfo.UserName,
                            EducationPlace = User_Education_Time_Object.Education.EducationName,
                            EducationLine = User_Education_Time_Object.EducationLine.EducationLineName,
                            CourseName = CourseNameOut,
                            CharacterValue = CharacterValueOut,
                            WhichCharecterScale = WhichCharacterScaleOut,
                            EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                            EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString(),
                            AbsencePercentage = AbsencePercentageOut
                        };
                        jSon_Object = ListItem;
                    }
                    else
                    {
                        var ListItem = new
                        {
                            ErrorCode = Const.ObjectNotSavedByCurrentUserOriginally,
                            ErrorText = "User Time Character Objekt er ikke gemt af nuværende bruger oprindeligt !!!"
                        };
                        jSon_Object = ListItem;
                    }
                }
                else
                {
                    var ListItem = new
                    {
                        ErrorCode = Const.ObjectNotFound,
                        ErrorText = "User Time Character Objekt er ikke fundet !!!"
                    };
                    jSon_Object = ListItem;
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.UserNotFound,
                    ErrorText = "Bruger er ikke fundet !!!"
                };
                jSon_Object = ListItem;
            }
            return (jSon_Object);
        }

        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            User_Education_Time_Collection User_Education_Time_Collection_Object = 
                new User_Education_Time_Collection();
            int NumberOfUserEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                User_Education_Time_Collection_Object.UserInfoID = UserID;
                User_Education_Time_Collection_Object.EducationID = json_Object.EducationID;
                User_Education_Time_Collection_Object.StartDate = json_Object.StartDate;
                User_Education_Time_Collection_Object.EndDate = json_Object.EndDate;
                User_Education_Time_Collection_Object.EducationLineID = json_Object.EducationLineID;

                //db.ContactForms.Add(ContactForm_Object);
                //NumberOfContactFormsSaved = db.SaveChanges();

                //if (1 == NumberOfContactFormsSaved)
                //{
                //    return (ContactForm_Object.ContactFormID);
                //}
                //else
                //{
                //    return (Const.SaveOperationFailed);
                //}
                return (Const.SaveOperationFailed);
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
        public void Delete(int id)
        {
        }
    }
}