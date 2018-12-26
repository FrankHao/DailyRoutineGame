namespace KidsTodo.Network
{
    using System;
    using System.Collections;

    using SimpleJSON;

    using UnityEngine;
    using UnityEngine.Networking;

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

        public IEnumerator GetRequest(string uri)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.Send();

            if (request.isNetworkError)
            {
                Debug.LogError("request error");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                if (request.responseCode == 200)
                {
                    Debug.Log("request finished successfully!");
                }
                else if (request.responseCode == 401)
                {
                    Debug.Log("Error 401: Unauthorized.");
                }
            }
        }

        public IEnumerator PostRequest(string uri, string bodyJsonString)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, bodyJsonString);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.Send();
        }

        public void TestConnect()
        {
            StartCoroutine(NetworkManager.Instance.GetRequest("http://httpbin.org/get"));
        }
    }
}


