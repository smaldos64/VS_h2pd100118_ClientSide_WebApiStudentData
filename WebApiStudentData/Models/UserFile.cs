using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class UserFile
    {
        public int UserFileID { get; set; }

        [Required]
        public int UserInfoID { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        [Required]
        public string UserFileUrl { get; set; }

        public string userFileAlt { get; set; }

    }
}