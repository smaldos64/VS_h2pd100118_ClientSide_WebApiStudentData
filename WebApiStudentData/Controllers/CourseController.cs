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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            Course Course_Object = new Course();
            int NumberOfCoursesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
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
            else
            {
                return (Const.UserNotFound);
            }
        }

        // PUT api/<controller>/5
        public int Put(dynamic json_Object, int id, string UserName, string Password)
        {
            Course Course_Object = new Course();
            int NumberOfCoursesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
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
            else
            {
                return (Const.UserNotFound);
            }
        }

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