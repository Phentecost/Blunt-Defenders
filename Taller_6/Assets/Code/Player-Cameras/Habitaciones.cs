using UnityEngine;

public class Habitaciones: MonoBehaviour
{
    public Transform target; // El objeto hacia el cual la cámara se moverá.
    public float smoothSpeed = 5f; // Velocidad de suavizado del movimiento de la cámara.
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Offset de la cámara respecto al personaje.

    private bool isFollowing = false; // Variable para rastrear si se debe seguir al personaje.

    void Update()
    {
        // Verificar si el Collider se ha tocado (por ejemplo, por medio de un OnTriggerEnter).
        if (isFollowing)
        {
            // Calcula la posición deseada de la cámara con suavizado.
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            // Actualiza la posición de la cámara.
            transform.position = smoothedPosition;

            // Mira hacia el personaje.
            transform.LookAt(target);
        }
    }

    // Método para activar el seguimiento de la cámara cuando el Collider se toca.
    public void StartFollowing()
    {
        isFollowing = true;
    }

    // Método para desactivar el seguimiento de la cámara.
    public void StopFollowing()
    {
        isFollowing = false;
    }
}
