using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cam_Player_System : MonoBehaviour
{
    public int cam_01,cam_02;
    public int cam_to_Go ;
    Vector2 last_Entry;
    enum Dir
    {
        X,Y
    }

    [SerializeField] private Dir Axis;

    void Start()
    {
        cam_to_Go = cam_01;
    }

    void invert()
    {
        if(cam_to_Go == cam_01)
        {
            cam_to_Go = cam_02;
        }
        else
        {
            cam_to_Go = cam_01;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PJ controller = other.GetComponentInChildren<PJ>();
        if(controller != null)
        {
            Camera_Manager.Instance.Change_Camera(cam_to_Go);
            last_Entry = transform.InverseTransformDirection(other.transform.position - transform.position);
            Debug.Log(last_Entry);
            invert();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player_Interaction controller = other.GetComponentInChildren<Player_Interaction>();
        if(controller != null)
        {
            var direction = transform.InverseTransformDirection(other.transform.position - transform.position);
            Debug.Log(direction);
            switch(Axis)
            {
                case Dir.X:
                
                if(direction.x < 0 && last_Entry.x < 0)
                {
                    
                    Camera_Manager.Instance.Change_Camera(cam_to_Go);
                    invert();
                }
                else if(direction.x > 0 && last_Entry.x > 0)
                {
                    
                    Camera_Manager.Instance.Change_Camera(cam_to_Go);
                    invert();
                }

                break;

                case Dir.Y:

                if(direction.y < 0 && last_Entry.y < 0)
                {
                    
                    Camera_Manager.Instance.Change_Camera(cam_to_Go);
                    invert();
                }
                else if(direction.y > 0 && last_Entry.y > 0)
                {
                    
                    Camera_Manager.Instance.Change_Camera(cam_to_Go);
                    invert();
                }

                break;
            }
        }
    }

    
}
