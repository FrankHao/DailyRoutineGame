
namespace KidsTodo.Login
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.SceneManagement;
    using TMPro;

    using KidsTodo.Common.Signals;
    using KidsTodo.Common.Network;

    public class LoginPage : MonoBehaviour
    {

        public GameObject usernameInput;
        public GameObject passwordInput;

        public string GetInputUserName()
        {
            var inputName = usernameInput.GetComponent<TMP_InputField>();
            return inputName.text;
        }

        public string GetInputPassword()
        {
            var inputPassword = passwordInput.GetComponent<TMP_InputField>();
            return inputPassword.text;
        }

        /// <summary>
        /// On login button pressed, send signal to login data manager.
        /// </summary>
        public void OnLoginPressed()
        {
            string username = GetInputUserName();
            string password = GetInputPassword();

            Signals.Get<LoginSignal>().Dispatch(username, password);
        }
    }
}


