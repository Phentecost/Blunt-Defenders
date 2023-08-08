using UnityEngine;

public class PJ : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 
    public Camera mainCamera; 

    private Vector2 touchOrigin = -Vector2.one;
    private bool canMove = true;

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

                Vector3 cameraPosition = new Vector3(playerRigidbody.position.x, playerRigidbody.position.y, mainCamera.transform.position.z);
                mainCamera.transform.position = cameraPosition;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchOrigin = -Vector2.one;
            }
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
