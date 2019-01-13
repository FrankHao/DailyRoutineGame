namespace KidsTodo.Login
{
    using UnityEngine;
    public class LoginView : MonoBehaviour
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


