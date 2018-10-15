using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class Absence
    {
        public int AbsenceID { get; set; }

        [Required]
        public int AbsencePercentage { get; set; }

        [Required]
        public int User_Education_Time_CollectionID { get; set; }
        public virtual User_Education_Time_Collection User_Education_Time_Collection { get; set; }
    }
}