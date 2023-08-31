using UnityEngine;
using UnityEngine.InputSystem;

public class PJ : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 
    public Camera mainCamera; 

    private Vector2 touchOrigin = -Vector2.one;
    private bool canMove = true;

    public PlayerInput playerInput;

    void Update()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending) return;

        Vector2 touchDirection = playerInput.actions["Walk"].ReadValue<Vector2>();
        touchDirection.Normalize();
        Vector2 movement = touchDirection * moveSpeed * Time.deltaTime; 
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
        Vector3 cameraPosition = new Vector3(playerRigidbody.position.x, playerRigidbody.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = cameraPosition;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }



    public Collider roomCollider; 
    public Habitaciones cameraController;

    private void OnTriggerEnter(Collider other)
    {
        if (other == roomCollider)
        {
            cameraController.target = roomCollider.transform;
        }
    }

 }