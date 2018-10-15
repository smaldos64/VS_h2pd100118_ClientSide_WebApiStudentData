using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public virtual ICollection<User_Education_Character_Course_Collection> User_Education_Character_Course_Collections { get; set; }
    }
}