using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;
using System.Web.Http.Cors;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]
    public class LogDataController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle/de fleste Database operationer. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle/de fleste Database operationer med tilhørende UserName. Listen 
        /// returneres som en liste af jSon objekter, hvor hver enkelt jSon element 
        /// indeholder felterne : LogDataID, LogDataDateTime, LogDataUserName, ThisDataBaseOperation og
        /// ThisModelDatabaseNumber.
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<LogData> LogData_List = new List<LogData>();

            LogData_List = db.LogDatas.ToList();

            foreach (LogData LogData_Object in LogData_List)
            {
                var ListItem = new
                {
                    LogDataID = LogData_Object.LogDataID,
                    LogDataDateTime = LogData_Object.LogDataDateTime,
                    LogDataUserName = LogData_Object.LogDataUserName,
                    ThisDataBaseOperationString  = Const.FindDataBaseOperationString(LogData_Object.ThisDataBaseOperation),
                    ThisDataBaseTableSTring = Const.FindTableOperationString(LogData_Object.ThisModelDatabaseNumber)
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        // GET: api/LogData/5
        public List<Object> Get(string UserName)
        {
            List<object> jSonList = new List<object>();
            List<LogData> LogData_List = new List<LogData>();

            LogData_List = db.LogDatas.Where(u => u.LogDataUserName == UserName).ToList();

            foreach (LogData LogData_Object in LogData_List)
            {
                var ListItem = new
                {
                    LogDataID = LogData_Object.LogDataID,
                    LogDataDateTime = LogData_Object.LogDataDateTime,
                    LogDataUserName = LogData_Object.LogDataUserName,
                    ThisDataBaseOperationString = Const.FindDataBaseOperationString(LogData_Object.ThisDataBaseOperation),
                    ThisDataBaseTableSTring = Const.FindTableOperationString(LogData_Object.ThisModelDatabaseNumber)
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        // POST: api/LogData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LogData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LogData/5
        public void Delete(int id)
        {
        }
    }
}
