using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiStudentData.Tools;

namespace WebApiStudentData.Models
{
    public class UserInfo
    {
        public int UserInfoID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }

        public static int FindUserInDatabase(string UserNanme, string Password)
        {
            DatabaseContext db = new DatabaseContext();

            List<UserInfo> UserInfo_List = db.UserInfos.ToList();
            Password = Crypto.Encrypt(Password);

            int UserCounter = 0;
            int UserID = 0;
                      
            while (UserCounter < UserInfo_List.Count)
            {
                if ( (UserInfo_List[UserCounter].UserName == UserNanme) &&
                     (UserInfo_List[UserCounter].UserPassword == Password) )
                {
                    UserID = UserInfo_List[UserCounter].UserInfoID;
                    break;
                }
                UserCounter++;
            }

            return (UserID);
        }
    }
}