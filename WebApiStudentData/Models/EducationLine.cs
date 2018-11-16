using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }
}