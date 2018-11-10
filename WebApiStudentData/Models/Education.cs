using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class Education
    {
        public int EducationID { get; set; }

        public string EducationName { get; set; }

        //public virtual ICollection<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }

        public virtual ICollection<EducationLine> EducationLines { get; set; }
    }
}