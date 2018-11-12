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

        /// <summary>
        /// Returnerer en oversigt over alle return koder og return stringe fra WEB Api'et.
        /// Alle return koder der har en værdi mindre end 0 signalerer en fejl.
        /// Alle return koder der har en værdi større end 0 signalerer, at operationen er gået godt.
        /// </summary>
        /// <returns></returns>
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

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}