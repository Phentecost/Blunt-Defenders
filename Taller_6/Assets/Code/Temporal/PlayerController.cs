using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float _Horizontal,_Vertical;
    
    // Update is called once per frame
    void Update()
    {
        _Horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        _Vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 movedPos = new Vector2(_Horizontal,_Vertical);
        transform.position += movedPos;

        Camera.main.transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }

    
}
