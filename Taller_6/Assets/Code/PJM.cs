using UnityEngine;

public class PJM : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 

    private Vector2 touchOrigin = -Vector2.one;
    private bool canMove = true;
    public Collider roomCollider; // Collider de la habitación actual
    public camara cameraController;

    private void OnTriggerEnter(Collider other)
    {
        if (other == roomCollider)
        {
            cameraController.target = roomCollider.transform;
        }
    }

    void Update()
    {

        if (Input.touchCount > 0 && canMove)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchOrigin = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && touchOrigin != -Vector2.one)
            {
                Vector2 touchEnd = touch.position;
                Vector2 touchDirection = touchEnd - touchOrigin;
                touchDirection.Normalize();

                Vector2 movement = touchDirection * moveSpeed * Time.deltaTime;

                
                playerRigidbody.MovePosition(playerRigidbody.position + movement);
              
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchOrigin = -Vector2.one;
            }
            
            
            
        }
    }

}