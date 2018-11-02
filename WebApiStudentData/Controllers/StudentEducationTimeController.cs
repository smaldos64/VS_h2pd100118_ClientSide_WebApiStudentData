using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;

namespace WebApiStudentData.Controllers
{
    public class StudentEducationTimeController : ApiController
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
                        EducationPlace = User_Education_Time_Object.Education.EducationName,
                        EducationLine = User_Education_Time_Object.EducationLine.EducationLineName,
                        EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                        EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString()
                    };

                    jSonList.Add(ListItem);
                }
            }
            return (jSonList);
        }


        // GET api/<controller>/5
        public List<object> Get(int id, string UserName, string Password)
        {
            List<object> jSonList = new List<object>();
            List<User_Education_Time_Collection> User_Education_Time_List = new List<User_Education_Time_Collection>();
            User_Education_Time_Collection User_Education_Time_Object = new User_Education_Time_Collection();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (0 < UserID)
            {
                if (null != db.User_Education_Time_Collections.FirstOrDefault(u => u.UserInfoID == UserID && u.User_Education_Time_CollectionID == id))
                {
                    User_Education_Time_Object = db.User_Education_Time_Collections.First(u => u.UserInfoID == UserID && u.User_Education_Time_CollectionID == id);

                    var ListItem = new
                    {
                        EducationID = User_Education_Time_Object.User_Education_Time_CollectionID,
                        UserInfoID = User_Education_Time_Object.UserInfoID,
                        UserName = User_Education_Time_Object.UserInfo.UserName,
                        EducationPlace = User_Education_Time_Object.Education.EducationName,
                        EducationLine = User_Education_Time_Object.EducationLine.EducationLineName,
                        EducationStartTime = User_Education_Time_Object.StartDate.ToShortDateString(),
                        EducationStopTime = User_Education_Time_Object.EndDate.ToShortDateString()
                    };
                    jSonList.Add(ListItem);
                }
            }
            return (jSonList);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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