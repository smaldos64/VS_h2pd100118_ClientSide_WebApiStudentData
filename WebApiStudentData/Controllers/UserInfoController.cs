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

        // GET api/values/5
        public int Get(string UserName, string Password)
        {
            object jSon_Object = new object();
            UserInfo UserInfo_Object = db.UserInfos.FirstOrDefault(u => u.UserName.ToLower() == UserName.ToLower() &&
                                       u.UserPassword.ToLower() == Password.ToLower());

            if (null != UserInfo_Object)
            {
                return (UserInfo_Object.UserInfoID);
            }
            else
            {
                return (Const.UserNotFound);
            }
        }

        // POST api/values
        public int Post(dynamic json_Object)
        {
#if (DEBUG)
            UserInfo UserInfo_Object = new UserInfo();
            int NumberOfUsersSaved;

            string PlainText = json_Object.UserPassword;

            UserInfo_Object.UserName = json_Object.UserName;
            UserInfo_Object.UserPassword = Crypto.Encrypt(PlainText);

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
#else
            return (Const.FeatureNotImplemented);
#endif
        }

        // PUT api/values/5
        public int Put(dynamic json_Object, string UserName, string Password)
        {
            UserInfo UserInfo_Object = new UserInfo();
            int NumberOfUsersSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                if (Const.UserNotFound == UserInfo.CheckForUserInDatabase(UserID, json_Object.UserName))
                {
                    UserInfo_Object = db.UserInfos.Find(UserID);
                    UserInfo_Object.UserName = json_Object.UserName;
                    string PlainText = json_Object.UserPassword;
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
            else
            {
                return (Const.UserNotFound);
            }
        }

        // DELETE api/values/5
        public int Delete(string UserName, string Password)
        {
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
        }
    }
}
