using System.Collections;
using UnityEngine;

public class Habitaciones: MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 0.125f; 
    public Vector3 offset; 

    private void LateUpdate()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending) return;
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
