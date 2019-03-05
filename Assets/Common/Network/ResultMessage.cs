namespace KidsTodo.Common.Network
{
    public class ResultMessage
    {
        public const string KIDS_TO_DO_SUCCESS = "KTDSuccess";
        public const string KIDS_TO_DO_RESULT = "KTDResult";
        public const string KIDS_TO_DO_ERRORMSG = "KTDErrorMsg";

        public const int LOGIN_RESULT_MESSAGE = 1;

        public int MessageId;
        public bool Success;
        public string Result;
        public string ErrorMsg;

        public ResultMessage()
        {
        }

        public ResultMessage(int id)
        {
            MessageId = id;
            Success = false;
            Result = "";
            ErrorMsg = "";
        }
    }

    public class LoginResultMessage: ResultMessage
    {
        public LoginResultMessage() :
            base(LOGIN_RESULT_MESSAGE)
        {
        }
    }
}
