using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private readonly AISystem aiSystem;
    private readonly Stack<Node> path;

    //Movement Commands
    private readonly MoveUp up;
    private readonly MoveLeft left;
    private readonly MoveDown down;
    private readonly MoveRight right;

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
            yield return new WaitForFixedUpdate();
        }
        
        aiSystem.SetState(new IdleState(aiSystem));
    }

    private void MoveTo(Node target)
    {
        Vector3 currentPos = aiSystem.transform.position;
        Vector3 targetPos = target.worldPos;

        if (Vector3.Distance(currentPos, targetPos) <= .1f)
        {
            path.Pop();
            return;
        }
        
        if (currentPos.x < targetPos.x - .05f) right.Execute();
        if (currentPos.x > targetPos.x + .05f) left.Execute();
        if (currentPos.y < targetPos.y - .05f) up.Execute();
        if (currentPos.y > targetPos.y + .05f) down.Execute();
    }
}
