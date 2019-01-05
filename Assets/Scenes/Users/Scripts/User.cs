
namespace KidsTodo.User
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Network;

    public class User : MonoBehaviour
    {

        private void OnMouseDown()
        {
            Debug.Log("OnClick");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("BirdGame");
          
        }

        public void Jump()
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        }

        public void Shake()
        {

        }


    }
}

