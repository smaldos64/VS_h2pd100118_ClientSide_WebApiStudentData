using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;

namespace WebApiStudentData.Controllers
{
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
                    CoursetName = Course_Object.CourseName
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

            if (0 < UserID)
            {
                if (0 == Course.CheckForCourseNameInCourse((string)json_Object.CourseName))
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
                        return (0);
                    }
                }
                else
                {
                    return (-2);
                }
            }
            else
            {
                return (-1);
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