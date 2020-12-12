using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private Transform wayPoint;

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

        pathfinding.wayPoint = wayPoint;
    }
    
    void Update()
    {
        MoveTo(pathfinding.path.Peek());
    }

    private void MoveTo(Node target)
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
        
        // Check if indentity reached the target position
        if (currentPos == target.gridPos)
        {
            pathfinding.path.Pop();
            return;
        }

        // Movement
        if (currentPos.x < target.gridPos.x)
        {
            right.Execute();
        }
        if (currentPos.y >= target.gridPos.x)
        {
            left.Execute();
        }
        if (currentPos.y < target.gridPos.y)
        {
            up.Execute();
        }
        if (currentPos.y >= target.gridPos.y)
        {
            down.Execute();
        }
    }
}
