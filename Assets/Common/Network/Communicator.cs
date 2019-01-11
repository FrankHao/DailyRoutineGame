using SimpleJSON;
using UnityEngine;

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
        public const string DevelopmentUrl = "http://127.0.0.1:8000/";

        public delegate void OnCommunicationResponse(ResultMessage msg);

        public OnCommunicationResponse CommunicationResponse;

        public void Login(string username, string password)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);
            string uri = BackendUrl + "api/v1/rest-auth/login/";
            StartCoroutine(NetworkManager.Instance.PostRequest(uri, form, OnLoginResponse));
        }

        public void OnLoginResponse(NetworkResponse response)
        {
            var data = JSON.Parse(response.ResponseData);
            ResultMessage msg = new LoginResultMessage();
            if (response.Type != ResponseType.Success)
            {
                msg.Success = false;
                msg.ErrorMsg = response.ResponseData;
            }
            else
            {
                JSONNode errorNode = data["non_field_errors"];
                if (errorNode != null)
                {
                    msg.Success = false;
                    msg.ErrorMsg = errorNode[0].Value;
                }
                else
                {
                    msg.Success = true;
                    msg.Result = "Logged In";
                }
            }

            if (CommunicationResponse != null)
            {
                CommunicationResponse(msg);
            }
        }
    }
}
