namespace Sat.Recruitment.Api.Common
{
    public static class Constants
    {       
        public const string USER_DUPLICATED = "The user is duplicated";
        public const string USER_USERTYPE_INCORRECT = "The userType is incorrect";
        public const string USER_EMAIL_INCORRECT = "The email cannot be normalized";
        public const string USER_MONEY_INCORRECT = "The money value is not parseable";
        public const string USER_FILE_PATH_IS_INCORRECT = "File path is incorrect";

        public const string USER_NAME_REQUIRED = "The name is required";
        public const string USER_EMAIL_REQUIRED = "The email is required";
        public const string USER_ADDRESS_REQUIRED = "The address is required";
        public const string USER_PHONE_REQUIRED = "The phone is required";
        public const string USER_USERTYPE_REQUIRED = "The userType is required";
        public const string USER_MONEY_REQUIRED = "The money is required";

        public const string USER_FILE_PATH = "/Files/Users.txt";
        public const string USER_FILE_PARAMS_SEPARATOR = ",";
    }
}
