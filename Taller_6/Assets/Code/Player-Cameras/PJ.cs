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



    public Transform newCameraPosition; // La nueva posición de la cámara.
    public float transitionSpeed = 5.0f; // Velocidad de transición de la cámara.

    private Camera Habitaciones; // Referencia a la cámara principal.

    private void Start()
    {
        // Obtén la referencia a la cámara principal.
        Habitaciones = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el collider ha colisionado con el jugador u otro objeto que puedas definir como desencadenante.
        if (other.CompareTag("Player")) // Asegúrate de ajustar la etiqueta según tu caso.
        {
            // Cambia la posición de la cámara hacia la nueva posición con suavizado.
            Vector3 targetPosition = newCameraPosition.position;
            Habitaciones.transform.position = Vector3.Lerp(Habitaciones.transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        }

    }
        
    
 }