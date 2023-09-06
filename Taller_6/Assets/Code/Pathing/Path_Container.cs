using System.Collections;
using System.Collections.Generic;
using System.IO;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;

public class Path_Container : MonoBehaviour
{
    public int Index;
    
    void OnTriggerExit2D(Collider2D other)
    {
        Player_Interaction controller = other.GetComponentInChildren<Player_Interaction>();
        if(controller != null)
        {
            controller._Can_Deploy = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player_Interaction controller = other.GetComponentInChildren<Player_Interaction>();
        if(controller != null)
        {
            controller._Can_Deploy = false;
        }
    }
}
