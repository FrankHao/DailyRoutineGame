namespace KidsTodo.Common.Network
{
    public abstract class ResultMessage
    {
        public const int LOGIN_RESULT_MESSAGE = 1;

        public int MessageId;
        public bool Success;
        public string Result;
        public string ErrorMsg;
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
