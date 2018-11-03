using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.ConstDeclarations
{
    public class Const
    {
        public static readonly string PasswordHash = "P@@Sw0rd";
        public static readonly string SaltKey = "S@LT&KEY";
        public static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public const int ObjectNotFound = -7;
        public const int CourseAlreadyPresent = -6;
        public const int SaveOperationFailed = -5;
        public const int UpdateOperationFailed = -4;
        public const int DeleteOperationFailed = -3;
        public const int UserNotFound = -2;
        public const int CourseNotFound = -1;
        public const int OperationOkHigherValueThanHere = 0;
        public const int UpdateOperationOk = 1;
        public const int SaveOperationOk = 2;
        public const int DeleteOperationOk = 3;
    }
}