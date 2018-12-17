﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log("OnClick");
        Jump();
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
    }

    public void Shake()
    {

    }
}