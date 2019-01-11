namespace KidsTodo.Common.Network
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using SimpleJSON;

    using UnityEngine;
    using UnityEngine.Networking;

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
                    lock (typeof(NetworkManager))
                    {
                        if (_instance == null)
                        {
                            _instance = new NetworkManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public delegate void RequestResponseDelegate(NetworkResponse response);

        public IEnumerator GetRequest(string uri, RequestResponseDelegate onResponse = null)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();
            NetworkResponse response = new NetworkResponse();
            if (request.isNetworkError)
            {
                Debug.LogError("request error");
                response.Type = ResponseType.NetworkError;
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                response.Type = ResponseType.Success;
                response.ResponseData = request.downloadHandler.text;
                if (request.responseCode == 200)
                {
                    Debug.Log("request finished successfully!");
                }
                else if (request.responseCode == 401)
                {
                    Debug.Log("Error 401: Unauthorized.");
                }
            }

            if (onResponse != null)
            {
                onResponse.Invoke(response);
            }
        }

        public IEnumerator PostRequest(string url, WWWForm form, RequestResponseDelegate onResponse = null)
        {
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
                onResponse.Invoke(response);
            }
        }

        public IEnumerator PostRequest(string uri, string bodyJsonString, RequestResponseDelegate onResponse = null)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, bodyJsonString);
            request.SetRequestHeader("Content-Type", "application/json");
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
                onResponse.Invoke(response);
            }
        }

        public IEnumerator PostRequest(string url, List<IMultipartFormSection> formData, RequestResponseDelegate onResponse = null)
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
                onResponse.Invoke(response);
            }
        }

        public IEnumerator PutRequest(string url, string sendData, RequestResponseDelegate onResponse = null)
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
                onResponse.Invoke(response);
            }
        }

        public IEnumerator DeleteRequest(string url, RequestResponseDelegate onResponse = null)
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
                onResponse.Invoke(response);
            }
        }

        public void TestConnect()
        {
            StartCoroutine(NetworkManager.Instance.GetRequest("http://httpbin.org/get"));
        }
    }
}


