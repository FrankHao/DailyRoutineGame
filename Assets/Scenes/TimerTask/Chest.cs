using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        GetComponent<Animator>().Play("ChestAnim");
    }
}
