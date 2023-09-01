using UnityEngine;
using UnityEngine.InputSystem;

public class PJ : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 
    public Camera mainCamera; 
    public PlayerInput playerInput;

    void Update()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending) return;

        Vector2 touchDirection = playerInput.actions["Walk"].ReadValue<Vector2>();
        touchDirection.Normalize();
        Vector2 movement = touchDirection * moveSpeed * Time.deltaTime; 
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }   
 }