using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class User_Education_Time_Collection
    {
        public int User_Education_Time_CollectionID { get; set; }

        [Required]
        public int UserInfoID { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        //[Required]
        //public int EducationID { get; set; }
        //public virtual Education Education { get; set; }

        [Required]
        public int EducationLineID { get; set; }
        public virtual EducationLine EducationLine { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        public float? AbsencePercentageEducation { get; set; }

        public int? WhichCharacterScaleID { get; set; }
        public virtual WhichCharacterScale WhichCharacterScale { get; set; }

        public int? CharacterValueEducation { get; set; }

        public virtual ICollection<User_Education_Character_Course_Collection> User_Education_Character_Course_Collection { get; set; }

        public static int FindEducationTime_Collection_With_Specified_EducationLineID(int EducationLineID)
        {
            DatabaseContext db = new DatabaseContext();

            int NumberOfEducationLinesFound = db.User_Education_Time_Collections.Where(c => c.EducationLineID == EducationLineID).ToList().Count;

            return (NumberOfEducationLinesFound);
        }
    }
}