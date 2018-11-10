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
                                                    "Ikke Oplyst !!!",
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
                                                    "Ikke Oplyst !!!",
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
                            ErrorText = "UserFile Objekt er ikke gemt af nuværende bruger oprindeligt !!!"
                        };
                        jSon_Object = ListItem;
                    }
                }
                else
                {
                    var ListItem = new
                    {
                        ErrorCode = Const.ObjectNotFound,
                        ErrorText = "UserFile Objekt er ikke fundet !!!"
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
        public void Post([FromBody]string value)
        {
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