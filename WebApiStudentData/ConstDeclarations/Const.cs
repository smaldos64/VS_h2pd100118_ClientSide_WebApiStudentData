using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApiStudentData.Models;

namespace WebApiStudentData.ConstDeclarations
{
    public enum WhichCharacterScaleENUM
    {
        Character_7_ENUM = 1,
        Character_13_ENUM = 2
    }

    public class Const
    {
        public static readonly DataBaseOperationInfo[] DataBaseOperationInfoArray =
        {
            new DataBaseOperationInfo(ThisDataBaseOperation: DataBaseOperation.SaveData_Enum, ThisDataBaseOperationString: "Gemme Data"),
            new DataBaseOperationInfo(ThisDataBaseOperation: DataBaseOperation.DeleteData_Enum, ThisDataBaseOperationString: "Slette Data"),
            new DataBaseOperationInfo(ThisDataBaseOperation: DataBaseOperation.UpdateData_Enum, ThisDataBaseOperationString: "Opdatere Data"),
            new DataBaseOperationInfo(ThisDataBaseOperation: DataBaseOperation.ReadData_Enum, ThisDataBaseOperationString: "Læse Data"),
        };
        public static string FindDataBaseOperationString(DataBaseOperation CurrentDataBaseOperation)
        {
            int ReturnStringCounter = 0;

            do
            {
                if (DataBaseOperationInfoArray[ReturnStringCounter].ThisDataBaseOperation == CurrentDataBaseOperation)
                {
                    return (DataBaseOperationInfoArray[ReturnStringCounter].ThisDataBaseOperationString);
                }
                else
                {
                    ReturnStringCounter++;
                }
            } while (ReturnStringCounter < DataBaseOperationInfoArray.Length);

            return ("Ingen string fundet der hører til søgt nummer !!!");
        }

        public static readonly TableOperationInfo[] TableOperationInfoArray =
        {
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.Absence_Enum, ThisModelDatabaseNumberString: "Absence Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.Character7Scale_Enum, ThisModelDatabaseNumberString: "Character7Scale Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.Character13Scale_Enum, ThisModelDatabaseNumberString: "Character13Scale Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.ContactForm_Enum, ThisModelDatabaseNumberString: "Contactform Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.Course_Enum, ThisModelDatabaseNumberString: "Course Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.EducationLine_Enum, ThisModelDatabaseNumberString: "EducationLine Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.Education_Enum, ThisModelDatabaseNumberString: "Education Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.UserFile_Enum, ThisModelDatabaseNumberString: "UserFile Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.UserInfo_Enum, ThisModelDatabaseNumberString: "UserInfo Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.User_Education_Character_Course_Collection_Enum, ThisModelDatabaseNumberString: "User_Education_Character_Course_Collection Tabel"),
            new TableOperationInfo(ThisModelDatabaseNumber: ModelDatabaseNumber.User_Eductaion_Time_Collection_Enum, ThisModelDatabaseNumberString: "User_Eductaion_Time_Collection Tabel")
        };

        public static string FindTableOperationString(ModelDatabaseNumber CurrentModelDatabaseNumber)
        {
            int ReturnStringCounter = 0;

            do
            {
                if (TableOperationInfoArray[ReturnStringCounter].ThisModelDatabaseNumber == CurrentModelDatabaseNumber)
                {
                    return (TableOperationInfoArray[ReturnStringCounter].ThisModelDatabaseNumberString);
                }
                else
                {
                    ReturnStringCounter++;
                }
            } while (ReturnStringCounter < TableOperationInfoArray.Length);

            return ("Ingen string fundet der hører til søgt nummer !!!");
        }

        public static readonly string PasswordHash = "P@@Sw0rd";
        public static readonly string SaltKey = "S@LT&KEY";
        public static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public const int UserAlreadySignedUpForEducation = -17;
        public const int WrongjSonObjectParameters = -16;
        public const int SpecifiedContentStillInUseInTablesBelow = -15;
        public const int WrongCharacterScaleProvided = -14;
        public const int WrongCharacterProvided = -13;
        public const int NoCharacterProvidedButCharacterScaleProvided = -12;
        public const int CharacterProvidedButNoCharacterScaleProvided = -11;
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

        public static readonly ReturnCodeAndReturnString[] ReturnCodesAndReturnStrings =
        {
            new ReturnCodeAndReturnString(ReturnCode : UserAlreadySignedUpForEducation, ReturnString : "Studerende er allerede tilmeldt dette Uddannelsesforløb på dette tidspunkt"),
            new ReturnCodeAndReturnString(ReturnCode : WrongjSonObjectParameters, ReturnString : "Én eller flere af de forventede parametre i det give jSon objekt mangler !!!"),
            new ReturnCodeAndReturnString(ReturnCode : SpecifiedContentStillInUseInTablesBelow, ReturnString : "ID i denne tabel der ønsket slettet er stadigvæk i brug i underliggende tabeller. Slet i disse tabeller først !!!"),
            new ReturnCodeAndReturnString(ReturnCode : WrongCharacterScaleProvided, ReturnString : "Forkert ID for karakterskale angivet"),
            new ReturnCodeAndReturnString(ReturnCode : WrongCharacterProvided, ReturnString : "Forkert karakterværdi i forhold til valgt karakterskala angivet"),
            new ReturnCodeAndReturnString(ReturnCode : NoCharacterProvidedButCharacterScaleProvided, ReturnString : "Ingen karakterværdi angivet selvom karakterskala er angivet"),
            new ReturnCodeAndReturnString(ReturnCode : CharacterProvidedButNoCharacterScaleProvided, ReturnString : "Ingen karakterskala angivet selvom karakterværdi er angivet"),
            new ReturnCodeAndReturnString(ReturnCode : InformationNotProvided, ReturnString : "Information er ikke gemt"),
            new ReturnCodeAndReturnString(ReturnCode : ObjectNotSavedByCurrentUserOriginally, ReturnString : "Objekt er ikke gemt af nuværende bruger oprindeligt !!!"),
            new ReturnCodeAndReturnString(ReturnCode : UserNameAlreadyPresent, ReturnString : "Brugernavn eksisterer allerede !!!"),
            new ReturnCodeAndReturnString(ReturnCode : FeatureNotImplemented, ReturnString : "Feature er ikke implementeret/er ikke enabled !!!"),
            new ReturnCodeAndReturnString(ReturnCode : ObjectNotFound, ReturnString : "objekt er ikke fundet !!!"),
            new ReturnCodeAndReturnString(ReturnCode : ObjectAlreadyPresent, ReturnString : "objekt er allerede tilgængelig !!!"),
            new ReturnCodeAndReturnString(ReturnCode : SaveOperationFailed, ReturnString : "Fejl under lagring af objekt !!!"),
            new ReturnCodeAndReturnString(ReturnCode : UpdateOperationFailed, ReturnString : "Fejl under opdatering af objekt !!!"),
            new ReturnCodeAndReturnString(ReturnCode : DeleteOperationFailed, ReturnString : "Fejl under sletning af objekt !!!"),
            new ReturnCodeAndReturnString(ReturnCode : UserNotFound, ReturnString : "Bruger ikke fundet !!!"),
            new ReturnCodeAndReturnString(ReturnCode : OperationOkHigherValueThanHere, ReturnString : "Returværdier større end denne værdi er ok returværdier"),
            new ReturnCodeAndReturnString(ReturnCode : UpdateOperationOk, ReturnString : "Objekt er opdateret korrekt"),
            new ReturnCodeAndReturnString(ReturnCode : SaveOperationOk, ReturnString : "Objekt er gemt korrekt"),
            new ReturnCodeAndReturnString(ReturnCode : DeleteOperationOk, ReturnString : "Objekt er slettet korrekt")
        };

        public static string FindReturnString(int ReturnCode)
        {
            int ReturnStringCounter = 0;

            do
            {
                if (ReturnCodesAndReturnStrings[ReturnStringCounter].ReturnCode == ReturnCode)
                {
                    return (ReturnCodesAndReturnStrings[ReturnStringCounter].ReturnString);
                }
                else
                {
                    ReturnStringCounter++;
                }
            } while (ReturnStringCounter < ReturnCodesAndReturnStrings.Length);

            return ("Ingen string fundet der hører til søgt nummer !!!");
        }
    }

    public class ReturnCodeAndReturnString
    {
        public int ReturnCode;
        public string ReturnString;

        public ReturnCodeAndReturnString(int ReturnCode, string ReturnString)
        {
            this.ReturnCode = ReturnCode;
            this.ReturnString = ReturnString;
        }
    }
}