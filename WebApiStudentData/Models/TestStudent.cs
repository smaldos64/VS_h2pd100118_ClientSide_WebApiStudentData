using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class TestStudent
    {
        public int TestStudentID { get; set; }
        public string TestStudentName { get; set; }

        public virtual ICollection<TestCourse> TestCourses { get; set; }
    }
}