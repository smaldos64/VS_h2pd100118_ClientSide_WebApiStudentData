using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Models
{
    public class Education
    {
        public int EducationID { get; set; }

        public string EducationName { get; set; }

        //public virtual ICollection<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }

        public virtual ICollection<EducationLine> EducationLines { get; set; }

        public static int CheckForEducationNameInEducation(string EducationName)
        {
            DatabaseContext db = new DatabaseContext();

            List<Education> Education_List = db.Educations.ToList();
            int EducationCounter = 0;
            int EducationID = 0;

            while (EducationCounter < Education_List.Count)
            {
                if (Education_List[EducationCounter].EducationName.ToLower() == EducationName.ToLower())
                {
                    EducationID = Education_List[EducationCounter].EducationID;
                    return (Const.ObjectAlreadyPresent);
                }
                EducationCounter++;
            }

            return (Const.ObjectNotFound);
        }
    }
}