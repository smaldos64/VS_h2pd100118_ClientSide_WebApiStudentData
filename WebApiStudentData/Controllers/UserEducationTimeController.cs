using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;
using WebApiStudentData.ViewModels;

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
                        CourseCharacterList = new List<VM_User_Education_Character_Course_Collection>()
                        //CourseCharacterList = new List<User_Education_Character_Course_Collection>()
                    };

                    foreach (User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object in
                        User_Education_Time_Object.User_Education_Character_Course_Collection)
                    {
                        //var ListItemCourseCharacter = new
                        //{
                        //    User_Education_Character_Course_CollectionID =
                        //        User_Education_Character_Course_Collection_Object.User_Education_Character_Course_CollectionID,
                        //    CourseID = User_Education_Character_Course_Collection_Object.CourseID,
                        //    CourseName = User_Education_Character_Course_Collection_Object.Course.CourseName,
                        //    WhichCharacterScaleIDCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                        //                            User_Education_Character_Course_Collection_Object.WhichCharacterScaleID :
                        //                            Const.InformationNotProvided,
                        //    WhichCharacterScaleNameCourse = (null != User_Education_Character_Course_Collection_Object.WhichCharacterScaleID) ?
                        //                            User_Education_Character_Course_Collection_Object.WhichCharacterScale.WhichCharacterScaleName :
                        //                            "Ikke Oplyst !!!",
                        //    CharacterValueCourse = (null != User_Education_Character_Course_Collection_Object.CharacterValueCourse) ?
                        //                          User_Education_Character_Course_Collection_Object.CharacterValueCourse :
                        //                          Const.InformationNotProvided,
                        //    AbsencePercentageCourse = (null != User_Education_Character_Course_Collection_Object.AbsencePercentageCourse) ?
                        //                                User_Education_Character_Course_Collection_Object.AbsencePercentageCourse :
                        //                                Const.InformationNotProvided,
                            
                        //};

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
                            CourseCharacterList = new List<VM_User_Education_Character_Course_Collection>()
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
                User_Education_Time_Collection_Object.StartDate = json_Object.StartDate;
                User_Education_Time_Collection_Object.EndDate = json_Object.EndDate;
                User_Education_Time_Collection_Object.EducationLineID = json_Object.EducationLineID;

                if (null != json_Object.CharacterValueEducation)
                {
                    User_Education_Time_Collection_Object.CharacterValueEducation = json_Object.CharacterValueEducation;
                }
                else
                {
                    User_Education_Time_Collection_Object.CharacterValueEducation = null;
                }

                if (null != json_Object.WhichCharacterScaleID)
                {
                    User_Education_Time_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                }
                else
                {
                    User_Education_Time_Collection_Object.WhichCharacterScaleID = null;
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
            else
            {
                return (Const.UserNotFound);
            }
        }

        // PUT api/<controller>/5
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            User_Education_Time_Collection User_Education_Time_Collection_Object =
                new User_Education_Time_Collection();
            int NumberOfUserEducationsSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                User_Education_Time_Collection_Object = db.User_Education_Time_Collections.Find(id);

                if (null != User_Education_Time_Collection_Object)
                {
                    User_Education_Time_Collection_Object.StartDate = json_Object.StartDate;
                    User_Education_Time_Collection_Object.EndDate = json_Object.EndDate;
                    User_Education_Time_Collection_Object.EducationLineID = json_Object.EducationLineID;

                    if (null != json_Object.CharacterValueEducation)
                    {
                        User_Education_Time_Collection_Object.CharacterValueEducation = json_Object.CharacterValueEducation;
                    }
                    else
                    {
                        User_Education_Time_Collection_Object.CharacterValueEducation = null;
                    }

                    if (null != json_Object.WhichCharacterScaleID)
                    {
                        User_Education_Time_Collection_Object.WhichCharacterScaleID = json_Object.WhichCharacterScaleID;
                    }
                    else
                    {
                        User_Education_Time_Collection_Object.WhichCharacterScaleID = null;
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
            else
            {
                return (Const.UserNotFound);
            }
        }

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