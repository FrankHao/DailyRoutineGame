
namespace KidsTodo.Login
{
    using System.Collections;
    using System.Collections.Generic;

    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    using KidsTodo.Common.Signals;

    public class RegisterPage : MonoBehaviour
    {
        [SerializeField]
        protected GameObject username;

        [SerializeField]
        protected GameObject gender;

        [SerializeField]
        protected GameObject age;

        public void OnRegisterConfirmPressed()
        {

            string nameValue = username.GetComponent<TMP_InputField>().text;
            int genderValue = gender.GetComponent<Dropdown>().value;
            //int ageValue = age.GetComponent<TMP_InputField>().text;
            int ageValue = 0;

            Signals.Get<RegisterSignal>().Dispatch(nameValue, genderValue, ageValue);
        }
    }
}


