
namespace KidsTodo.Login
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class LoginViewManager : MonoBehaviour
    {
        public GameObject loginPage;
        public GameObject registerPage;

        public void ShowRegisterPage()
        {
            loginPage.SetActive(false);

            registerPage.SetActive(true);
        }

        public void ShowLoginPage()
        {
            loginPage.SetActive(true);

            registerPage.SetActive(false);
        }
    }
}


