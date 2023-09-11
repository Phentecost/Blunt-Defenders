using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel_Interaction : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        Player_Interaction controller = other.GetComponentInChildren<Player_Interaction>();
        if(controller != null)
        {
            controller.Deny_Deply();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player_Interaction controller = other.GetComponentInChildren<Player_Interaction>();
        if(controller != null)
        {
            controller.Deny_Deply();
        }
    }
}
