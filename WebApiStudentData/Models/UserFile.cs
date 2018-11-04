using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiStudentData.ConstDeclarations;

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

        public static int CheckForUserFileUrlInUserFile(int UserID, string UserFileUrl)
        {
            DatabaseContext db = new DatabaseContext();

            List<UserFile> UserFile_List = db.UserFiles.Where(u => u.UserInfoID == UserID).ToList();
            int UserFileCounter = 0;
            int UserFileID = 0;

            while (UserFileCounter < UserFile_List.Count)
            {
                if (UserFile_List[UserFileCounter].UserFileUrl.ToLower() == UserFileUrl.ToLower())
                {
                    UserFileID = UserFile_List[UserFileCounter].UserFileID;
                    break;
                }
                UserFileCounter++;
            }

            return (Const.ObjectNotFound);
        }
     }
}