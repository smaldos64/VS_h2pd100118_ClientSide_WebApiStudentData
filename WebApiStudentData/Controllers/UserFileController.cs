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