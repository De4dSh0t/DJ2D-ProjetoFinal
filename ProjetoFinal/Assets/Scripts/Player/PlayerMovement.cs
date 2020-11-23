using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CollisionDetector collisionDetector;
    
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
        if (Input.GetKey(KeyCode.W))
        {
            if (collisionDetector.canMoveUp) up.Execute();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (collisionDetector.canMoveLeft) left.Execute();
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (collisionDetector.canMoveDown) down.Execute();
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (collisionDetector.canMoveRight) right.Execute();
        }
    }
}
