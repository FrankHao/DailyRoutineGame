
namespace KidsTodo.Login
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.SceneManagement;

    using KidsTodo.Common.Signals;
    using KidsTodo.Common.Network;

    public class LoginController : MonoBehaviour
    {
        #region Singleton
        private static LoginController _instance;
        public static LoginController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LoginController>();
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<LoginController>();
                        singletonObject.name = typeof(LoginController).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Reference of login view, update view when get data from server.
        /// </summary>
        public LoginView loginView;

        private void Awake()
        {
            AddSignalListener();
        }

        private void OnDestroy()
        {
            RemoveSignalListener();
        }

        #region Singals
        public void AddSignalListener()
        {
            Signals.Get<LoginSignal>().AddListener(OnLoginSignal);
            Signals.Get<RegisterSignal>().AddListener(OnRegisterSignal);
        }

        public void RemoveSignalListener()
        {
            Signals.Get<LoginSignal>().RemoveListener(OnLoginSignal);
            Signals.Get<RegisterSignal>().RemoveListener(OnRegisterSignal);
        }
        #endregion

        private void OnRegisterSignal(string username, int gender, int age)
        {

        }

        private void OnLoginSignal(string username, string password)
        {
            Debug.Log(string.Format("OnLoginSignal, ready to send data to server, {0}, {1}",
                username, password));
            Communicator.Instance.Login(username, password, OnLoggedIn);
        }

        /// <summary>
        /// Call back from login signal.
        /// </summary>
        /// <param name="msg">Message.</param>
        private void OnLoggedIn(ResultMessage msg)
        {
            Debug.Log(string.Format("OnLoggedIn, get result message, {0}", msg.MessageId));
            if (msg.MessageId == ResultMessage.LOGIN_RESULT_MESSAGE)
            {
                if (msg.Success)
                {
                    // TODO: use scene manager to manage multi scenes.
                    loginView.gameObject.SetActive(false);
                    SceneManager.LoadScene("Users", LoadSceneMode.Additive);
                }
            }
        }
    }
}
