using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class User_Education_Character_Course_Collection
    {
        public int User_Education_Character_Course_CollectionID { get; set; }

        [Required]
        public int User_Education_Time_CollectionID { get; set; }
        public virtual User_Education_Time_Collection User_Education_Time_Collection { get; set; }

        [Required]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        //[Required]
        public int? WhichCharacterScaleID { get; set; }
        public virtual WhichCharacterScale WhichCharacterScale { get; set; }

        //[Required]
        public int? CharacterValueCourse { get; set; }

        public float? AbsencePercentageCourse { get; set; }
    }
}