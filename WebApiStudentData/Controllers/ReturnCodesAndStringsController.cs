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
        /// </summary>
        /// <returns>
        /// Oversigt over sammenhørende return koder og return strenge :
        /// -15 (SpecifiedContentStillInUseInTablesBelow) : "ID i denne tabel der ønsket slettet er stadigvæk i brug i underliggende tabeller. Slet i disse tabeller først !!!" -> 
        /// -14 (WrongCharacterScaleProvided) : "Forkert ID for karakterskale angivet" -> 
        /// -13 (WrongCharacterProvided) : "Forkert karakterværdi i forhold til valgt karakterskala angivet" -> 
        /// -12 (NoCharacterProvidedButCharacterScaleProvided) : "Ingen karakterværdi angivet selvom karakterskala er angivet" -> 
        /// -11 (CharacterProvidedButNoCharacterScaleProvided) : "Ingen karakterskala angivet selvom karakterværdi er angivet" -> 
        /// -10 (InformationNotProvided) : "Information er ikke gemt" -> 
        ///  -9 (ObjectNotSavedByCurrentUserOriginally) : "Objekt er ikke gemt af nuværende bruger oprindeligt !!!" -> 
        ///  -8 (UserNameAlreadyPresent) : "Brugernavn eksisterer allerede !!!" -> 
        ///  -7 (FeatureNotImplemented) : "Feature er ikke implementeret/er ikke enabled !!!" -> 
        ///  -6 (ObjectNotFound) : "objekt er ikke fundet !!!" -> 
        ///  -5 (ObjectAlreadyPresent) : "objekt er allerede tilgængelig !!!" -> 
        ///  -4 (SaveOperationFailed) : "Fejl under lagring af objekt !!!" -> 
        ///  -3 (UpdateOperationFailed) : "Fejl under opdatering af objekt !!!" -> 
        ///  -2 (DeleteOperationFailed) : "Fejl under sletning af objekt !!!" -> 
        ///  -1 (UserNotFound) : "Bruger ikke fundet !!!"
        ///   0 (OperationOkHigherValueThanHere) : "Returværdier større end denne værdi er ok returværdier" -> 
        ///   1 (UpdateOperationOk) : "Objekt er opdateret korrekt" -> 
        ///   2 (SaveOperationOk) : "Objekt er gemt korrekt" -> 
        ///   3 (DeleteOperationOk) : "Objekt er slettet korrekt"
        /// </returns>
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