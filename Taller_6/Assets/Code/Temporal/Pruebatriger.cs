using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebatriger : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Sali");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("#X");
    }
}
