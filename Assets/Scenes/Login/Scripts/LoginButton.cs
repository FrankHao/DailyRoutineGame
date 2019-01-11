using KidsTodo.Common.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour 
{

    public void OnClick()
    {
        Communicator.Instance.CommunicationResponse += OnLoggedIn;
        GameObject go = GameObject.Find("/Canvas/LoginManager/LoginPage/UserInfoPanel/UserNameInput");
        TMP_InputField t = go.GetComponent<TMP_InputField>();
        string username = t.text;
        go = GameObject.Find("/Canvas/LoginManager/LoginPage/UserInfoPanel/PasswordInput");
        t = go.GetComponent<TMP_InputField>();
        string password = t.text;
        Communicator.Instance.Login(username, password);
    }

    private void OnLoggedIn(ResultMessage msg)
    {
        if (msg.MessageId == ResultMessage.LOGIN_RESULT_MESSAGE)
        {
            if (msg.Success)
            {
                var status = "Logged in!";
                UnityEngine.SceneManagement.SceneManager.LoadScene("Users");
            }
            else
            {
                var status = "Login error: " + msg.ErrorMsg;
            }
        }
    }
}
