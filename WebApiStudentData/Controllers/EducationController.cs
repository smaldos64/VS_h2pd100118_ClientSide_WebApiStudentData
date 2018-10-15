using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;

namespace WebApiStudentData.Controllers
{
    public class EducationController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<Education> Education_List = new List<Education>();

            Education_List = db.Educations.ToList();

            foreach (Education Education_Object in Education_List)
            {
                var ListItem = new
                {
                    EducationID = Education_Object.EducationID,
                    EducationPlace = Education_Object.EducationName,
                    EducationLine = Education_Object.EducationLine
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
        public int Post(dynamic json_Object)
        {
            Education Education_Object = new Education();
            int NumberOfUsersSaved;

            Education_Object.EducationName = json_Object.EducationPlace;
            Education_Object.EducationLine = json_Object.EducationLine;

            db.Educations.Add(Education_Object);
            NumberOfUsersSaved = db.SaveChanges();

            if (1 == NumberOfUsersSaved)
            {
                return (Education_Object.EducationID);
            }
            else
            {
                return (0);
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