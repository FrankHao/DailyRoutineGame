
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SimpleJSON;

using UnityEngine;
using UnityEngine.Networking;

namespace KidsTodo.Common.Network
{
    public enum ResponseType
    {
        /// <summary>
        /// ClientError, the client could not perform the request(eg. could not reach destination host)
        /// </summary>
        NetworkError,
        /// <summary>
        /// PageNotFound, the page could not be found(invalid url)
        /// </summary>
        PageNotFound,
        /// <summary>
        /// The server returned an error regarding the request, which can be invalid post data or invalid authentication for example
        /// </summary>
        RequestError,
        /// <summary>
        /// The server returned an error but the response body could not be parsed
        /// </summary>
        ParseError,
        /// <summary>
        /// Success, if the server returns content it will be parsed into a JObject or JArray.
        /// </summary>
        Success,
    }

    public class NetworkResponse
    {
        public ResponseType Type;
        public string ResponseData;
        public NetworkResponse()
        {
            Type = ResponseType.NetworkError;
            ResponseData = null;
        }
    }


    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<NetworkManager>();
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<NetworkManager>();
                        singletonObject.name = typeof(NetworkManager).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        private string Token = "";

        public delegate void RequestResponseDelegate(NetworkResponse response);

        public IEnumerator GetRequest(string uri, Action<UnityWebRequest> onResponse)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();
            if (onResponse != null)
            {
                onResponse(request);
            }
        }

        public IEnumerator PostRequest(string url, WWWForm form, Action<ResultMessage> onResponse)
        {
            //make sure we get a json response
            form.headers.Add("Accept", "application/json");
            //also add the correct request method
            form.headers.Add("X-UNITY-METHOD", "POST");
            UnityWebRequest request = UnityWebRequest.Post(url, form);
            yield return request.SendWebRequest();

            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
            }

            if (onResponse != null)
            {
                var resultMessage = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMessage);
            }
        }

        public IEnumerator PostRequest(string uri, string bodyJsonString, Action<ResultMessage> onResponse)
        {

            UnityWebRequest request = UnityWebRequest.Put(uri, bodyJsonString);
            request.method = "POST";
            request.SetRequestHeader("Cookie", "");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("X-UNITY-METHOD", "POST");
            UnityWebRequest.ClearCookieCache();
            yield return request.SendWebRequest();

            var headers = request.GetResponseHeaders();
            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
            }

            if (onResponse != null)
            {
                var resultMessage = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMessage);
            }
        }

        public IEnumerator PostRequest(string url, List<IMultipartFormSection> formData, Action<ResultMessage> onResponse = null)
        {
            UnityWebRequest request = UnityWebRequest.Post(url, formData);
            yield return request.SendWebRequest();
            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
            }

            if (onResponse != null)
            {
                var resultMessage = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMessage);
            }
        }

        public IEnumerator PutRequest(string url, string sendData, Action<ResultMessage> onResponse = null)
        {
            byte[] dataToPut = System.Text.Encoding.UTF8.GetBytes(sendData);
            UnityWebRequest request = UnityWebRequest.Put(url, dataToPut);
            yield return request.SendWebRequest();
            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
            }

            if (onResponse != null)
            {
                var resultMessage = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMessage);
            }
        }

        public IEnumerator DeleteRequest(string url, Action<ResultMessage> onResponse = null)
        {
            UnityWebRequest request = UnityWebRequest.Delete(url);
            yield return request.SendWebRequest();
            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.Log("Error While Sending: " + request.error);
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log("Deleted");
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
            }

            if (onResponse != null)
            {
                var resultMsg = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMsg);
            }
        }

        public void TestConnect()
        {
            StartCoroutine(NetworkManager.Instance.GetRequest("http://httpbin.org/get", null));
        }

        /// <summary>
        /// Called from other module, can we send multi post request at the same time?
        /// </summary>
        /// <param name="rtype">Rtype.</param>
        /// <param name="uri">URI.</param>
        /// <param name="form">Form.</param>
        /// <param name="callback">Callback.</param>
        public void SendPostRequest(string uri, WWWForm form, Action<ResultMessage> callback)
        {
            StartCoroutine(PostRequest(uri, form, callback));
        }

        public void SendPostRequest(string uri, string bodyJsonString, Action<ResultMessage> callback)
        {
            StartCoroutine(PostRequest(uri, bodyJsonString, callback));
        }

        public void SendGetRequest(string uri, Action<UnityWebRequest> callback)
        {
            StartCoroutine(GetRequest(uri, callback));
        }

        public void SendDeleteRequest(string uri, Action<ResultMessage> callback)
        {
            StartCoroutine(DeleteRequest(uri, callback));
        }

        public void SendPutRequest(string uri, string requestData, Action<ResultMessage> callback)
        {
            StartCoroutine(PutRequest(uri, requestData, callback));
        }

        public void SendRequestWithWWW(string url, string type, WWWForm wwwForm,
            Action<ResultMessage> onResponse = null)
        {
            WWW request;

            Dictionary<string, string> headers;

            byte[] postData;
            if (wwwForm == null)
            {
                wwwForm = new WWWForm();
                postData = new byte[] { 1 };
            }
            else
            {
                postData = wwwForm.data;
            }

            headers = wwwForm.headers;

            //make sure we get a json response
            headers.Add("Accept", "application/json");

            //also add the correct request method
            headers.Add("X-UNITY-METHOD", type.ToString().ToUpper());

            //also, add the authentication token, if we have one

            request = new WWW(url, postData, headers);

            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            string callee = stackTrace.GetFrame(1).GetMethod().Name;
            StartCoroutine(HandleRequest(request, onResponse));
        }

        private IEnumerator HandleRequest(WWW request, Action<ResultMessage> onResponse)
        {
            //Wait till request is done
            while (true)
            {
                if (request.isDone)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            NetworkResponse response = new NetworkResponse();
            response.ResponseData = request.text;
            //catch proper client errors(eg. can't reach the server)
            if (!String.IsNullOrEmpty(request.error))
            {
                if (onResponse != null)
                {
                    response.Type = ResponseType.NetworkError;
                }
                else
                {
                    response.Type = ResponseType.Success;
                }
            }

            var cookie = request.responseHeaders["cookie"];
            if (cookie != null)
            {
                var cookieString = cookie.Split(';');
                foreach (var s in cookieString)
                {
                    var item = s.Split('=');
                    if (item[0].Contains("csrftoken"))
                    {
                        Token = item[1];
                        break;
                    }
                }
            }

            //deal with successful responses
            if (onResponse != null)
            {
                var resultMsg = Communicator.Instance.GetResultMessage(response);
                onResponse(resultMsg);
            }
        }
    }
}


