using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiStudentData.Models;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Controllers
{
    public class EducationLineController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET api/<controller>
        public List<Object> Get()
        {
            List<object> jSonList = new List<object>();
            List<EducationLine> EducationLine_List = new List<EducationLine>();

            EducationLine_List = db.EducationLines.ToList();

            foreach (EducationLine EducationLine_Object in EducationLine_List)
            {
                var ListItem = new
                {
                    EducationLineID = EducationLine_Object.EducationLineID,
                    EducationLineName = EducationLine_Object.EducationLineName,
                    EducationPlace = EducationLine_Object.Education.EducationName
                };

                jSonList.Add(ListItem);
            }
            return (jSonList);
        }

        // GET api/<controller>/5
        public Object Get(int id)
        {
            object jSon_Object = new object();
            EducationLine EducationLine_Object = new EducationLine();

            EducationLine_Object = db.EducationLines.Find(id);

            if (null != EducationLine_Object)
            {
                var ListItem = new
                {
                    EducationLineID = EducationLine_Object.EducationLineID,
                    EducationLineName = EducationLine_Object.EducationLineName,
                    EducationName = EducationLine_Object.Education.EducationName
                };
                jSon_Object = ListItem;
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

            return (jSon_Object);
        }

        // POST api/<controller>
        public int Post(dynamic json_Object, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                EducationLine_Object.EducationLineName = json_Object.EducationLineName;
                EducationLine_Object.EducationID = json_Object.EducationID;

                db.EducationLines.Add(EducationLine_Object);
                NumberOfEducationLinesSaved = db.SaveChanges();

                if (1 == NumberOfEducationLinesSaved)
                {
                    return (EducationLine_Object.EducationID);
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
        public int Put(int id, dynamic json_Object, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesSaved;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                EducationLine_Object = db.EducationLines.Find(id);
                if (null != EducationLine_Object)
                {
                    EducationLine_Object.EducationLineName = json_Object.EducationName;
                    EducationLine_Object.EducationID = json_Object.EducationID;

                    NumberOfEducationLinesSaved = db.SaveChanges();
                    if (1 == NumberOfEducationLinesSaved)
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
                return (Const.UserNotFound);
            }
        }

        // DELETE api/<controller>/5
        public int Delete(int id, string UserName, string Password)
        {
            EducationLine EducationLine_Object = new EducationLine();
            int NumberOfEducationLinesDeleted;

            int UserID = 0;

            UserID = UserInfo.FindUserInDatabase(UserName, Password);

            if (Const.UserNotFound < UserID)
            {
                EducationLine_Object = db.EducationLines.Find(id);
                if (null != EducationLine_Object)
                {
                    db.EducationLines.Remove(EducationLine_Object);
                    NumberOfEducationLinesDeleted = db.SaveChanges();
                    if (1 == NumberOfEducationLinesDeleted)
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