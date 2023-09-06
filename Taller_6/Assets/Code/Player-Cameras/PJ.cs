using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PJ : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 
    public PlayerInput playerInput;
    Vector2 movement;

    void Update()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending) return;

        Vector2 touchDirection = playerInput.actions["Walk"].ReadValue<Vector2>();
        touchDirection.Normalize();
        movement = touchDirection * moveSpeed * Time.deltaTime; 
        
    }   
    
    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }
 }