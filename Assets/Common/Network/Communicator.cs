
using System;

using UnityEngine;

using SimpleJSON;

namespace KidsTodo.Common.Network
{
    /// <summary>
    /// The type of the request(sets the HTTP method)
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// GET
        /// </summary>
        Get,
        /// <summary>
        /// POST
        /// </summary>
        Post,
        /// <summary>
        /// PUT
        /// </summary>
        Put,
        /// <summary>
        /// DELETE
        /// </summary>
        Delete
    }

    class Communicator : MonoBehaviour
    {
        private static Communicator _instance;
        public static Communicator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Communicator>();
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<Communicator>();
                        singletonObject.name = typeof(NetworkManager).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        //---- Public Properties ----//
        public static string BackendUrl
        {
            get
            {
                return UseProduction ? ProductionUrl : DevelopmentUrl;
            }
        }

        //---- URLS ----//
        public const bool UseProduction = false;
        public const bool Secure =false;
        public const string ProductionUrl = "http://foobar:8000/api/";
        public const string DevelopmentUrl = "http://127.0.0.1:8080/";

        public void Login(string username, string password, Action<ResultMessage> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
            string uri = BackendUrl + "api/v1/rest-auth/login/";
            NetworkManager.Instance.SendPostRequest(uri, form, callback);
        }
    }
}
