using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.ConstDeclarations;
using WebApiStudentData.Models;

namespace WebApiStudentData.Controllers
{
    public class UserFileController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        /// <summary>
        /// Returnerer info om alle Brugerfiler gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>Liste af Brugerfiler. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/<controller>
        public List<Object> Get(string UserName, string Password)
        {
            List<object> jSonList = new List<object>();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                List<UserFile> UserFileList = new List<UserFile>();
                UserFileList = db.UserFiles.Where(u => u.UserInfoID == UserID).ToList();

                foreach (UserFile UserFile_Object in UserFileList)
                {
                    var ListItem = new
                    {
                        UserFileID = UserFile_Object.UserFileID,
                        UserFileUrl = UserFile_Object.UserFileUrl,
                        UserFileAtt = UserFile_Object.userFileAlt
                    };
                    jSonList.Add(ListItem);
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorNumber = Const.UserNotFound
                };
                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        /// <summary>
        /// Returnerer info om én Brugerfil udfra id gemt af en bruger specificeret ved UserName 
        /// og Password.
        /// </summary>
        /// <param name="id">Integer der specificerer id på kontaktformular.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>Én Brugerfil. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // GET api/<controller>/5
        public object Get(int id, string UserName, string Password)
        {
            object jSon_Object = new object();
            UserFile UserFile_Object = new UserFile();
            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                UserFile_Object = db.UserFiles.Find(id);

                if (null != UserFile_Object)
                {
                    if (UserID == UserFile_Object.UserInfoID)
                    {
                        var ListItem = new
                        {
                            UserFileID = UserFile_Object.UserFileID,
                            UserFileUrl = UserFile_Object.UserFileUrl,
                            UserFileAtt = UserFile_Object.userFileAlt
                        };
                        jSon_Object = ListItem;
                    }
                    else
                    {
                        var ListItem = new
                        {
                            ErrorCode = Const.ObjectNotSavedByCurrentUserOriginally,
                            ErrorText = Const.FindReturnString(Const.ObjectNotSavedByCurrentUserOriginally)
                        };
                        jSon_Object = ListItem;
                    }
                }
                else
                {
                    var ListItem = new
                    {
                        ErrorCode = Const.ObjectNotFound,
                        ErrorText = Const.FindReturnString(Const.ObjectNotFound)
                    };
                    jSon_Object = ListItem;
                }
            }
            else
            {
                var ListItem = new
                {
                    ErrorCode = Const.UserNotFound,
                    ErrorText = Const.FindReturnString(Const.UserNotFound)
                };
                jSon_Object = ListItem;
            }
            return (jSon_Object);
        }

        /// <summary>
        /// Gemmer Brugerfil hørende til bruger specificeret ved UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige.
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : UserFileUrl og UserFileAlt. 
        /// </param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// Id nummeret på den gemte Brugerfil. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            UserFile UserFile_Object = new UserFile();
            int NumberOfUserFilesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                UserFile_Object.UserFileID = UserID;
                UserFile_Object.UserFileUrl = json_Object.UserFileUrl;
                UserFile_Object.userFileAlt = json_Object.UserFileAlt;

                db.UserFiles.Add(UserFile_Object);
                NumberOfUserFilesSaved = db.SaveChanges();

                if (1 == NumberOfUserFilesSaved)
                {
                    return (UserFile_Object.UserFileID);
                }
                else
                {
                    return (Const.SaveOperationFailed);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Ændrer Brugerfil hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// Kontaktformular med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="json_Object">json_Objekt er et objekt i jSon format. Det skal indeholde 
        /// data til funktionen med følgende felter specificeret : UserFileUrl og UserFileAlt.
        /// </param>
        /// <param name="id">Integer der specificerer id på kontaktformular.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// UpdateOperationOk (værdien 1) hvis Brugerfil er gemt ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // PUT api/<controller>/5
        public int Put(dynamic json_Object, int id, string UserName, string Password)
        {
            UserFile UserFile_Object = new UserFile();
            int NumberOfUserFilesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if (Const.ObjectNotFound == UserFile.CheckForUserFileUrlInUserFile(UserID, (string)json_Object.UserFileUrl))
                {
                    UserFile_Object = db.UserFiles.Find(id);
                    if (null != UserFile_Object)
                    {
                        UserFile_Object.UserInfoID = UserID;
                        UserFile_Object.UserFileUrl = json_Object.UserFileUrl;
                        UserFile_Object.userFileAlt = json_Object.UserFileAtt;

                        NumberOfUserFilesSaved = db.SaveChanges();
                        if (1 == NumberOfUserFilesSaved)
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
                        return (Const.ObjectNotFound);
                    }
                }
                else
                {
                    return (Const.ObjectAlreadyPresent);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        /// <summary>
        /// Sletter Brugerfil hørende til bruger specificeret ved id, UserName og Password.  
        /// </summary>
        /// <remarks>
        /// UserName og Password skal være gemt i Web API'ets database for at være gyldige. Og 
        /// Kontaktformular med specificeret id, skal være gemt af nuværende bruger før. 
        /// </remarks>
        /// <param name="id">Integer der specificerer id på kontaktformular.</param>
        /// <param name="Password">Password for nuværende bruger.</param>
        /// <param name="UserName">Brugernavn for nuværende bruger.</param>
        /// <returns>
        /// DeleteOperationOk (værdien 3) hvis Brugerfil er slettet ok. 
        /// Eller en retur kode med en værdi mindre end 0, hvis noget gik galt. 
        /// Se en oversigt over return koder i ReturnCodesAndStrings eller klik 
        /// her : <see cref="ReturnCodeAndReturnString"/>
        /// </returns>
        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            UserFile UserFile_Object = new UserFile();
            int NumberOfUserFilesDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                UserFile_Object = db.UserFiles.Find(id);
                if (null != UserFile_Object)
                {
                    db.UserFiles.Remove(UserFile_Object);
                    NumberOfUserFilesDeleted = db.SaveChanges();
                    if (1 == NumberOfUserFilesDeleted)
                    {
                        return (Const.DeleteOperationOk);
                    }
                    else
                    {
                        return (Const.DeleteOperationFailed);
                    }
                }
                else
                {
                    return (Const.ObjectNotFound);
                }
            }
            else
            {
                return (Const.UserNotFound);
            }
        }
    }
}