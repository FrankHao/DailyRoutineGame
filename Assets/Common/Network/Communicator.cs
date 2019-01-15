using System;
using UnityEngine;
using SimpleJSON;

namespace KidsTodo.Common.Network
{
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

        public void Login(string username, string password, Action<ResultMessage> callback)
        {
            JSONObject node = new JSONObject();
            node.Add("username", username);
            node.Add("password", password);
            string uri = BackendUrl + "api/v1/rest-auth/login/";
            NetworkManager.Instance.SendPostRequest(uri, node.ToString(), callback);
        }

        public ResultMessage GetResultMessage(NetworkResponse response)
        {
            ResultMessage msg = new ResultMessage();
            if (response.Type != ResponseType.Success)
            {
                msg.Success = false;
                msg.ErrorMsg = response.ResponseData;
            }
            else
            {
                JSONNode data = JSON.Parse(response.ResponseData);
                JSONNode errorNode = data[ResultMessage.KIDS_TO_DO_RESULT];
                if (errorNode != null)
                {
                    bool success;
                    if (bool.TryParse(data[ResultMessage.KIDS_TO_DO_SUCCESS].Value, out success))
                    {
                        msg.Success = success;
                    }
                    msg.Result = errorNode[ResultMessage.KIDS_TO_DO_RESULT].Value;
                    msg.ErrorMsg = errorNode[ResultMessage.KIDS_TO_DO_ERRORMSG].Value;
                }
                else
                {
                    msg.Success = true;
                    msg.Result = response.ResponseData;
                }
            }
            return msg;
        }
    }
}
