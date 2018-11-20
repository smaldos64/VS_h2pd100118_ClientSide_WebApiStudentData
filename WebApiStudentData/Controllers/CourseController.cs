using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.Tools;
using WebApiStudentData.ConstDeclarations;
using System.Web.Http.Cors;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]

    public class CourseController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Fag/Kurser. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle Fag/Kurser med tilhørende Fag/Kursus ID.
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<Course> Course_List = new List<Course>();

            Course_List = db.Courses.ToList();

            foreach (Course Course_Object in Course_List)
            {
                var ListItem = new
                {
                    CourseID = Course_Object.CourseID,
                    CourseName = Course_Object.CourseName
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer info om ét specifikt Fag/Kursus udfra ID
        /// </summary>
        /// <param name="id">Fag/Kursus ID</param>
        /// <returns>
        /// Returnerer Kursus/Fag navn udfra Kursus/Fag ID
        /// </returns>
        // GET api/<controller>/5
        public object Get(int id)
        {
            object jSon_Object = new object();
            Course Course_Object = new Course();

            Course_Object = db.Courses.Find(id);

            if (null != Course_Object)
            {
                var ListItem = new
                {
                    CourseID = Course_Object.CourseID,
                    CourseName = Course_Object.CourseName
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
        /// Gemmer et nyt Fag/Kursus. Ved kald af denne funktionalitet skal man angive
        /// Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : CourseName</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på det gemte Fag/Kursus. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            Course Course_Object = new Course();
            int NumberOfCoursesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if (null == json_Object.CourseName)
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    if (Const.ObjectNotFound == Course.CheckForCourseNameInCourse((string)json_Object.CourseName))
                    {
                        Course_Object.CourseName = json_Object.CourseName;

                        db.Courses.Add(Course_Object);
                        NumberOfCoursesSaved = db.SaveChanges();

                        if (1 == NumberOfCoursesSaved)
                        {
                            return (Course_Object.CourseID);
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
        /// Opdaterer ét Fag/Kursus udfra Fag/Kursus ID. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : CourseName</param>
        /// <param name="id">Fag/Kursus ID</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Kursus/Fag er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(dynamic json_Object, int id, string UserName, string Password)
        {
            Course Course_Object = new Course();
            int NumberOfCoursesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if (null == json_Object.CourseName)
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    if (Const.ObjectNotFound == Course.CheckForCourseNameInCourse((string)json_Object.CourseName))
                    {
                        Course_Object = db.Courses.Find(id);
                        if (null != Course_Object)
                        {
                            Course_Object.CourseName = json_Object.CourseName;
                            NumberOfCoursesSaved = db.SaveChanges();
                            if (1 == NumberOfCoursesSaved)
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
        /// Sletter ét specifikt Fag/Kursus udfra Fag/Kursus ID. Ved kald af denne funktionalitet skal man 
        /// angive Brugernavn og Passsword. Kun brugere kendt af systemet kan udnytte denne 
        /// funktionalitet.
        /// </summary>
        /// <param name="id">Fag/Kursus ID</param>
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
            Course Course_Object = new Course();
            int NumberOfCoursesDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {

                Course_Object = db.Courses.Find(id);
                if (null != Course_Object)
                {
                    if (0 == User_Education_Character_Course_Collection.FindEducation_Course_Character_Collection_With_Specified_CourseID(id))
                    {
                        db.Courses.Remove(Course_Object);
                        NumberOfCoursesDeleted = db.SaveChanges();
                        if (1 == NumberOfCoursesDeleted)
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