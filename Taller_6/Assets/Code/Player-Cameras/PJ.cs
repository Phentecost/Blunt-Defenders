using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PJ : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D playerRigidbody; 
    public PlayerInput playerInput;
    Vector2 movement;
    [SerializeField] private Animator animator;

    [SerializeField] private ParticleSystem particulas;

    public static PJ Instance {get;private set;} = null;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if(Game_Manager._Current_Game_State == Game_Manager.Game_State.Defending) return;

        Vector2 touchDirection = playerInput.actions["Walk"].ReadValue<Vector2>();
        touchDirection.Normalize();
        movement = touchDirection * moveSpeed * Time.deltaTime; 
        animator.SetFloat("X",touchDirection.x);
        animator.SetFloat("Y",touchDirection.y);
        if(touchDirection != Vector2.zero) 
        {
            particulas.Play();
            animator.SetBool("Moving",true);
            
        }
        else
        {
            animator.SetBool("Moving",false);
        }
    }   
    
    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + movement);
       
    }
 }