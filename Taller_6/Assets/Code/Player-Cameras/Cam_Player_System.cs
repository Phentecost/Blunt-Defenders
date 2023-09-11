using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_Player_System : MonoBehaviour
{
    public int cam_01;
    void OnTriggerEnter2D(Collider2D other)
    {
        PJ controller = other.GetComponentInChildren<PJ>();
        if(controller != null)
        {
            Camera_Manager.Instance.Change_Camera(cam_01);
            controller.transform.position = transform.GetChild(0).position;
        }
    }

    

    
}
