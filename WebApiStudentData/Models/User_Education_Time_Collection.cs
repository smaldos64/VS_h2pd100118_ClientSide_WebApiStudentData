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

        [Required]
        public int EducationID { get; set; }
        public virtual Education Education { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<User_Education_Character_Course_Collection> User_Education_Character_Course_Collection { get; set; }

        public virtual ICollection<Absence> Absences { get; set; }
    }
}