using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Fly("Up");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Fly("Down");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Fly("Left");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Fly("Right");
        }
    }

    public void Fly(string dir)
    {
        switch(dir)
        {
            case "Up":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 8f));
                break;
            case "Down":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -8f));
                break;
            case "Left":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-8f, 0f));
                break;
            case "Right":
                GetComponent<Rigidbody2D>().AddForce(new Vector2(8f, 0f));
                break;
            default:
                break;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.GetComponent<Boom>())
        {
            gameObject.SetActive(false);
        }

        if (collision2D.gameObject.name.Contains("Saw"))
        {
            GetComponent<Animator>().Play("BirdCrash");
        }
    }
}
