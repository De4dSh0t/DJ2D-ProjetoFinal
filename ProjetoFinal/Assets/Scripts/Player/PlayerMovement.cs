using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
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
    }
    
    void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            up.Execute();
        }

        if (Input.GetKey(KeyCode.A))
        {
            left.Execute();
        }

        if (Input.GetKey(KeyCode.S))
        {
            down.Execute();
        }

        if (Input.GetKey(KeyCode.D))
        {
            right.Execute();
        }
    }
}
