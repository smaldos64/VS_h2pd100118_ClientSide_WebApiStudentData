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

    public class Character7ScaleController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om karakterværdier på 7-trins skalaen. Brug denne info til at
        /// angive en korrekt karakterværdi hvis 7-trins skalaen er valgt som karakterskala.
        /// </summary>
        /// <returns>
        /// Returnerer en liste af tilgængelige karakterværdier på 7-trins skalaen med tilhørende 
        /// karakterværdi ID. Listen returneres som en liste jSon objekter, hvor hver enkelt 
        /// jSon element indeholder felterne : Character7ScaleID og Character7ScaleValue. 
        /// Gyldig karakterværdi i forhold til valgt karakterskala, skal angives alle steder,
        /// hvor der indsættes karakterer !!!
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<Character7Scale> Character7Scale_List = new List<Character7Scale>();

            Character7Scale_List = db.Character7Scales.ToList();

            foreach (Character7Scale Character7Scale_Object in Character7Scale_List)
            {
                var ListItem = new
                {
                    Character7ScaleID = Character7Scale_Object.Character7ScaleID,
                    Character7ScaleValue = Character7Scale_Object.Character7ScaleValue
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