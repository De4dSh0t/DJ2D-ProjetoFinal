using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private readonly AISystem aiSystem;
    private Stack<Node> path;
    private float timeToUpdate;
    
    //Movement Commands
    private MoveUp up;
    private MoveLeft left;
    private MoveDown down;
    private MoveRight right;

    public MoveState(AISystem system, Stack<Node> path)
    {
        this.path = path;
        aiSystem = system;
        
        up = new MoveUp(system.transform, system.speed);
        left = new MoveLeft(system.transform, system.speed);
        down = new MoveDown(system.transform, system.speed);
        right = new MoveRight(system.transform, system.speed);
    }

    public IEnumerator Execute()
    {
        while (path.Count > 0)
        {
            MoveTo(path.Peek());
        }
        
        aiSystem.SetState(new IdleState(aiSystem));
        yield break;
    }

    private void MoveTo(Node target)
    {
        Vector3Int currentPos = Vector3Int.RoundToInt(aiSystem.transform.position);
        Vector3Int targetPos = target.gridPos;

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
