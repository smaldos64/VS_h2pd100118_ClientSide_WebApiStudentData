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

    public class Character13ScaleController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om karakterværdier på 13 karakter skalaen. Brug denne info til at
        /// angive en korrekt karakterværdi hvis 13 skalaen er valgt som karakterskala.
        /// </summary>
        /// <returns>
        /// Returnerer en liste af tilgængelige karakterværdier på 13 skalaen med tilhørende 
        /// karakterværdi ID. Gyldig karakterværdi i forhold til valgt karakterskala, skal 
        /// angives alle steder, hvor der indsættes karakterer !!!
        /// </returns>
        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<Character13Scale> Character13Scale_List = new List<Character13Scale>();

            Character13Scale_List = db.Character13Scales.ToList();

            foreach (Character13Scale Character13Scale_Object in Character13Scale_List)
            {
                var ListItem = new
                {
                    Character13ScaleID = Character13Scale_Object.Character13ScaleID,
                    Character13ScaleValue = Character13Scale_Object.Character13ScaleValue
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