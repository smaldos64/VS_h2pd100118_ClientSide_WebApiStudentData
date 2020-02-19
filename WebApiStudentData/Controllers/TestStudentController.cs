using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiStudentData.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]
    public class TestStudentController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        //public bool Post(dynamic json_Object)
        [HttpPost]
        public bool Post(TestStudent testStudent)
        {
            //manuelt filtrer og søg kurser fra database og vælg de rigtige id'er
            var courses = new List<TestCourse>();
            foreach (var testCourse in testStudent.TestCourses)
            {
                var dbCourse = db.TestCourses.FirstOrDefault(x => x.TestCourseName == testCourse.TestCourseName);
                courses.Add(dbCourse);
            }
            //opbyg student data container med kursus data fra database og student data fra postmetode 
            var newStudent = new TestStudent
            {
                TestStudentName = testStudent.TestStudentName,
                TestCourses = courses
            };
            //add student 
            db.TestStudents.Add(newStudent);
            //gem ændringer 
            int NumberOfTestStudentsSaved = db.SaveChanges();

            if (0 < NumberOfTestStudentsSaved)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public bool Put(int id, TestStudent testStudent)
        {
            var modifiedStudent = db.TestStudents.Find(id);
            if (null != modifiedStudent)
            {

                //manuelt filtrer og søg kurser fra database og vælg de rigtige id'er
                var courses = new List<TestCourse>();
                foreach (var testCourse in testStudent.TestCourses)
                {
                    var dbCourse = db.TestCourses.FirstOrDefault(x => x.TestCourseName == testCourse.TestCourseName);
                    courses.Add(dbCourse);
                }
                
                modifiedStudent.TestStudentName = testStudent.TestStudentName;
                modifiedStudent.TestCourses.Clear();
                modifiedStudent.TestCourses = courses;

                //gem ændringer 
                int NumberOfTestStudentsSaved = db.SaveChanges();

                if (0 < NumberOfTestStudentsSaved)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public bool Delete(int id)
        {
            var deletedStudent = db.TestStudents.Find(id);
            if (null != deletedStudent)
            {
                db.TestStudents.Remove(deletedStudent);
                var NumberOfTestSTtudentsDeleted = db.SaveChanges();
                if (1 == NumberOfTestSTtudentsDeleted)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            else
            {
                return (false);
            }
        }
    }
}