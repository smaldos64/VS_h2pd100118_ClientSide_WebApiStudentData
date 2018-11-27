#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiStudentData.Models;
using WebApiStudentData.Tools;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Controllers
{
    [EnableCors(origins: "*", headers: "Content-Type", methods: "GET,POST,PUT,DELETE,OPTIONS")]

    public class UserInfoController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Brugere. 
        /// </summary>
        /// <returns>
        /// Returnerer en liste af alle Brugere tilknyttet Web API'et. Listen returneres som en liste af jSon objekter, 
        /// hvor hver enkelt jSon element indeholder felterne : UserInfoID, UserName og UserPassword. 
        /// </returns>
        // GET api/values
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<UserInfo> UserInfo_List = new List<UserInfo>();

            UserInfo_List = db.UserInfos.ToList();

            foreach (UserInfo UserInfo_Object in UserInfo_List)
            {
#if (DEBUG)
                var ListItem = new
                {
                    UserInfoID = UserInfo_Object.UserInfoID,
                    UserName = UserInfo_Object.UserName,
                    UserPassword = Crypto.Decrypt(UserInfo_Object.UserPassword)
                };

                jSonList.Add(ListItem);
#endif

                var ListItemCrypt = new
                {
                    UserInfoID = UserInfo_Object.UserInfoID,
                    UserName = UserInfo_Object.UserName,
                    UserPassword = UserInfo_Object.UserPassword
                };

                jSonList.Add(ListItemCrypt);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer UserInfo ID for specificeret Bruger. 
        /// </summary>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Returnerer UserInfo ID for én specifik Bruger angivet ved UserName og Password. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/values/5
        public int Get(string UserName, string Password)
        {
            object jSon_Object = new object();
            string Password_Crypted = Crypto.Encrypt(Password);

            UserInfo UserInfo_Object = db.UserInfos.FirstOrDefault(u => UserName.ToLower() == UserName.ToLower() &&
                                       u.UserPassword == Password_Crypted);

            if (null != UserInfo_Object)
            {
                return (UserInfo_Object.UserInfoID);
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Opretter en Bruger specificeret ved UserName og Password. Denne funktion er ikke !!! 
        /// tilgængelig i den publicerede version af Web API'et.
        /// </summary>
        /// <param name="UserName">Brugernavn for bruger der ønsker oprettet.</param>
        /// <param name="Password">Password for bruger der ønsker oprettet.</param>
        /// <returns>
        /// UserInfo Id nummeret på den gemte Bruger. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/values
        [HttpPost]
        public int Post(string UserName, string Password)
        {
#if (DEBUG)
            UserInfo UserInfo_Object = new UserInfo();
            int NumberOfUsersSaved;

            string PlainText = Password;

            UserInfo_Object.UserName = UserName;
            UserInfo_Object.UserPassword = Crypto.Encrypt(PlainText);

            if (UserInfo.CheckForUserInDatabaseCreation(UserName))
            {
                return (Const.UserNameAlreadyPresent);
            }
            else
            {
                db.UserInfos.Add(UserInfo_Object);
                NumberOfUsersSaved = db.SaveChanges();

                if (1 == NumberOfUsersSaved)
                {
                    return (UserInfo_Object.UserInfoID);
                }
                else
                {
                    return (Const.SaveOperationFailed);
                }
            }
#else
            return (Const.FeatureNotImplemented);
#endif
        }

        /// <summary>
        /// Ændrer Brugernavn og/eller Password for en eksisterende Bruger specificeret ved nuværende 
        /// UserName og Password. De nye brugerinfo skal være angivet i parameteren json_Object.
        /// </summary>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : UserName og Password. Og her 
        /// skal det pointeres, at det er det nye Brugernavn og/eller Password, der skal være
        /// angivet !!!
        /// </param>
        /// <param name="UserName">Nuværende Brugernavn for bruger der ønsker Brugernavn ændret.</param>
        /// <param name="Password">Nuværende Password for bruger der ønsker Password ændret.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Bruger er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/values/5
        public int Put(dynamic json_Object, string UserName, string Password)
        {
            UserInfo UserInfo_Object = new UserInfo();
            int NumberOfUsersSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if ((null == json_Object.UserName) ||
                    (null == json_Object.Password))
                {
                    return (Const.WrongjSOnObjectParameters);
                }
                else
                {
                    if (Const.UserNotFound == UserInfo.CheckForUserInDatabase(UserID, (string)json_Object.UserName))
                    {
                        UserInfo_Object = db.UserInfos.Find(UserID);
                        UserInfo_Object.UserName = json_Object.UserName;
                        string PlainText = json_Object.Password;
                        UserInfo_Object.UserPassword = Crypto.Encrypt(PlainText);

                        NumberOfUsersSaved = db.SaveChanges();
                        if (1 == NumberOfUsersSaved)
                        {
                            return (Const.UpdateOperationOk);
                        }
                        else
                        {
                            return (Const.UpdateOperationFailed);
                        }
                    }
                    else
                    {
                        return (Const.ObjectAlreadyPresent);
                    }
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Sletter Bruger specificeret ved UserName og Password. Denne funktion er ikke !!! 
        /// tilgængelig i den publicerede version af Web API'et. 
        /// </summary>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Bruger er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/values/5
        public int Delete(string UserName, string Password)
        {
#if (DEBUG)
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                return (Const.FeatureNotImplemented);
            }
            else
            {
                return (Const.UserNotFound);
            }
#else
            return (Const.FeatureNotImplemented);
#endif
        }
    }
}
