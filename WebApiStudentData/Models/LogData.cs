using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{

    public enum DataBaseOperation
    {
        SaveData_Enum,
        UpdateData_Enum,
        DeleteData_Enum
    }

    public enum ModelDatabaseNumber
    {
        Absence_Enum,
        Character13Scale_Enum,
        Character7Scale_Enum,
        ContactForm_Enum,
        Course_Enum,
        Education_Enum,
        EducationLine_Enum,
        User_Education_Character_Course_Collection_Enum,
        User_Eductaion_Time_Collection_Enum,
        UserFile_Enum,
        UserInfo_Enum
    }

    public class LogData
    {
        public int LogDataID { get; set; }

        public DateTime LogDataDateTime { get; set; }

        public string LogDataUserName { get; set; }

        public DataBaseOperation ThisDataBaseOperation { get; set; }

        public ModelDatabaseNumber ThisModelDatabaseNumber { get; set; }

        private static DatabaseContext db = new DatabaseContext();

        public static bool LogDataToDatabase(string LogDataUserName, 
                                      DataBaseOperation ThisDataBaseOperation,
                                      ModelDatabaseNumber ThisModelDatabaseNumber)
        {
            int NumberOfLogDatasSaved = 0;
            LogData LogData_Object = new LogData();

            LogData_Object.LogDataDateTime = DateTime.Now;
            LogData_Object.LogDataUserName = LogDataUserName;
            LogData_Object.ThisDataBaseOperation = ThisDataBaseOperation;
            LogData_Object.ThisModelDatabaseNumber = ThisModelDatabaseNumber;

            db.LogDatas.Add(LogData_Object);
            NumberOfLogDatasSaved = db.SaveChanges();

            if (1 == NumberOfLogDatasSaved)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}