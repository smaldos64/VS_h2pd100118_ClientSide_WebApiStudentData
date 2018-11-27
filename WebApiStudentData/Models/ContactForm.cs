using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiStudentData.Models
{
    public class ContactForm
    {
        public int ContactFormID { get; set; }

        [Required]
        public int UserInfoID { get; set; }
        public virtual UserInfo UserInfo { get; set; }

        [Required]
        public string ContactNameFrom { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string ContactNameEmail { get; set; }

        [Required]
        public string ContactText { get; set; }

        public string ContactNamePhoneNumber { get; set; }

        public string ContactSubject { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        public string ContactEmailRecipient { get; set; }
    }
}