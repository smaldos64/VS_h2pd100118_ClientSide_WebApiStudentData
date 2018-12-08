using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiStudentData.ConstDeclarations
{
    public enum WhichCharacterScaleENUM
    {
        Character_7_ENUM = 1,
        Character_13_ENUM = 2
    }

    public class Const
    {
        public static readonly string PasswordHash = "P@@Sw0rd";
        public static readonly string SaltKey = "S@LT&KEY";
        public static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public const int UserAlreadySignedUpForEducation = -17;
        public const int WrongjSOnObjectParameters = -16;
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
            new ReturnCodeAndReturnString(ReturnCode : WrongjSOnObjectParameters, ReturnString : "Én eller flere af de forventede parametre i det give jSon objekt mangler !!!"),
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