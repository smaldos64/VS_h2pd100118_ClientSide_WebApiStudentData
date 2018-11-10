using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiStudentData.Models;

namespace WebApiStudentData.ViewModels
{
    public class VM_User_Education_Character_Course_Collection
    {
        public User_Education_Character_Course_Collection User_Education_Character_Course_Collection_Object { get; set; }

        public string WhichCharacterScaleName { get; set; }

        public string CourseName { get; set; }
    }
}