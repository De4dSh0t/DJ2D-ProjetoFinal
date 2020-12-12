using System.Collections.Generic;
using UnityEngine;

public class AIMove : IState, IWalk
{
    private AISystem aiSystem;
    private Stack<Node> path;
    
    //Movement Commands
    private MoveUp up;
    private MoveLeft left;
    private MoveDown down;
    private MoveRight right;

    public AIMove(AISystem system, Stack<Node> path)
    {
        this.path = path;
        aiSystem = system;
        up = new MoveUp(system.transform, system.speed);
        left = new MoveLeft(system.transform, system.speed);
        down = new MoveDown(system.transform, system.speed);
        right = new MoveRight(system.transform, system.speed);
    }

    public void Execute()
    {
        Debug.Log("Execute");
        Move(path);
    }

    public void Move(Stack<Node> path)
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(aiSystem.transform.position);
        Vector3Int targetPos = path.Peek().gridPos;

        if (currentPos == targetPos)
        {
            path.Pop();
            return;
        }
        
        if (currentPos.x < targetPos.x) right.Execute();
        if (currentPos.x > targetPos.x) left.Execute();
        if (currentPos.y < targetPos.y) up.Execute();
        if (currentPos.y > targetPos.y) down.Execute();
    }
}
