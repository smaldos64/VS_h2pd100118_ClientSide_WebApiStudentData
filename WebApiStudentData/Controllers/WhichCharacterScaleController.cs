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

    public class WhichCharacterScaleController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om Karakterskalaer.
        /// </summary>
        /// <returns>
        /// Returnerer en liste af tilgængelige karakterskalaer med tilhørende karakterskale ID. 
        /// Karakterskale ID skal angives alle steder, hvor der indsættes karakterer !!!
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<WhichCharacterScale> WhichCharacterScale_List = new List<WhichCharacterScale>();

            WhichCharacterScale_List = db.WhichCharacterScales.ToList();

            foreach (WhichCharacterScale WhichCharacterScale_Object in WhichCharacterScale_List)
            {
                var ListItem = new
                {
                    WhichCharacterScaleID = WhichCharacterScale_Object.WhichCharacterScaleID,
                    WhichCharacterScaleName = WhichCharacterScale_Object.WhichCharacterScaleName
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