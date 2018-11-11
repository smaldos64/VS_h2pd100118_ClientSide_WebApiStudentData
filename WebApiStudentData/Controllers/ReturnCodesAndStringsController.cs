using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiStudentData.ConstDeclarations;
using WebApiStudentData.Models;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]

    public class ReturnCodesAndStringsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();

            foreach (ReturnCodeAndReturnString ReturnCodeAndReturnString_Object in Const.ReturnCodesAndReturnStrings)
            {
                var ListItem = new
                {
                   ReturnNumber = ReturnCodeAndReturnString_Object.ReturnCode,
                   ReturnString = ReturnCodeAndReturnString_Object.ReturnString
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