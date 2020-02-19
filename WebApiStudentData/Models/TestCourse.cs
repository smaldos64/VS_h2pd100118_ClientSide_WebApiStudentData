using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class TestCourse
    {
        public int TestCourseID { get; set; }
        public string TestCourseName { get; set; }

        public virtual ICollection<TestStudent> TestStudents { get; set; }
    }
}