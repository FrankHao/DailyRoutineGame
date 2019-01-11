
namespace KidsTodo.Login
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using KidsTodo.Common.Signals;

    public class LoginDataManager
    {
        public void AddSignalListener()
        {
            //Signals.Get<LoginSignal>().AddListener();
        }

        private void OnLoginSignal(string username, string password)
        {

        }
    }
}
