using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiStudentData.ConstDeclarations;

namespace WebApiStudentData.Models
{
    public class EducationLine
    {
        public int EducationLineID { get; set; }

        public string EducationLineName { get; set; }

        [Required]
        public int EducationID { get; set; }
        public virtual Education Education { get; set; }

        public virtual ICollection<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }

        public static int FindEducationLine_With_Specified_EducationID(int EducationID)
        {
            DatabaseContext db = new DatabaseContext();

            int NumberOfEducationLinesFound = db.EducationLines.Where(c => c.EducationID == EducationID).ToList().Count;

            return (NumberOfEducationLinesFound);
        }

        public static int CheckForEducationLineNameInEducationLine(string EducationLineName, int EducationID)
        {
            DatabaseContext db = new DatabaseContext();

            List<EducationLine> EducationLine_List = db.EducationLines.ToList();
            int EducationLineCounter = 0;
            int EducationLineID = 0;

            while (EducationLineCounter < EducationLine_List.Count)
            {
                if ( (EducationLine_List[EducationLineCounter].EducationLineName.ToLower() == EducationLineName.ToLower()) &&
                     (EducationLine_List[EducationLineCounter].EducationID == EducationID) )
                {
                    EducationLineID = EducationLine_List[EducationLineCounter].EducationLineID;
                    return (Const.ObjectAlreadyPresent);
                }
                EducationLineCounter++;
            }

            return (Const.ObjectNotFound);
        }
    }
}