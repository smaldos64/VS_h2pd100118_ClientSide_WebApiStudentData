using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class EducationLine
    {
        public int EducationLineID { get; set; }

        public string EducationLineName { get; set; }

        public int EducationID { get; set; }
        public virtual Education Education { get; set; }

        public virtual ICollection<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }
    }
}