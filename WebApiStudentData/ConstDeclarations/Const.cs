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

        public const int InformationNotProvided = -10;
        public const int ObjectNotSavedByCurrentUserOriginally = -9;
        public const int UserNameAlreadyPresent = -8;
        public const int FeatureNotImplemented = -7;
        public const int ObjectNotFound = -6;
        public const int ObjectAlreadyPresent = -5;
        public const int SaveOperationFailed = -4;
        public const int UpdateOperationFailed = -3;
        public const int DeleteOperationFailed = -2;
        public const int UserNotFound = -1;
        public const int OperationOkHigherValueThanHere = 0;
        public const int UpdateOperationOk = 1;
        public const int SaveOperationOk = 2;
        public const int DeleteOperationOk = 3;
    }

    public class ErrorCodesAndErrorStrings
    {
        public int ErrorCode;
        public string ErrorString;
    }
}