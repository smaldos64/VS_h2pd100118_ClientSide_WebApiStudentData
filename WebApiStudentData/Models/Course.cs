using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public virtual ICollection<User_Education_Character_Course_Collection> User_Education_Character_Course_Collections { get; set; }

        public static int CheckForCourseNameInCourse(string CourseName)
        {
            DatabaseContext db = new DatabaseContext();

            List<Course> Course_List = db.Courses.ToList();
            int CourseCounter = 0;
            int CourseID = 0;

            while (CourseCounter < Course_List.Count)
            {
                if (Course_List[CourseCounter].CourseName.ToLower() == CourseName.ToLower())
                {
                    CourseID = Course_List[CourseCounter].CourseID;
                    break;
                }
                CourseCounter++;
            }

            return (Const.ObjectNotFound);
        }
    }
}