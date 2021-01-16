using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    private CollisionDetector collisionDetector;
    
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    private Vector2 movementVector;
    private bool isWalking;
    
    //Movement Commands
    private MoveUp up;
    private MoveLeft left;
    private MoveDown down;
    private MoveRight right;
    
    void Start()
    {
        up = new MoveUp(transform, speed);
        left = new MoveLeft(transform, speed);
        down = new MoveDown(transform, speed);
        right = new MoveRight(transform, speed);

        collisionDetector = GetComponent<CollisionDetector>();
    }
    
    void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        movementVector = Vector2.zero;
        isWalking = false;
        
        if (Input.GetKey(KeyCode.W))
        {
            if (collisionDetector.canMoveUp) up.Execute();
            movementVector += Vector2.up;
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (collisionDetector.canMoveLeft) left.Execute();
            movementVector += Vector2.left;
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (collisionDetector.canMoveDown) down.Execute();
            movementVector += Vector2.down;
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (collisionDetector.canMoveRight) right.Execute();
            movementVector += Vector2.right;
            isWalking = true;
        }
        
        // Animation
        Vector2 normalized = movementVector.normalized;
        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("Horizontal", normalized.x);
        animator.SetFloat("Vertical", normalized.y);
    }
}
