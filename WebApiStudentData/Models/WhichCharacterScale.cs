using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class WhichCharacterScale
    {
        public int WhichCharacterScaleID { get; set; }

        public string WhichCharacterScaleName { get; set; }

        public virtual ICollection<User_Education_Character_Course_Collection> User_Education_Character_Course_Collections { get; set; }

        public virtual ICollection<User_Education_Time_Collection> User_Education_Time_Collections { get; set; }
    }
}