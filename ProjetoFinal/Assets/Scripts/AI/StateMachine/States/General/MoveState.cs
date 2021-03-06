﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private readonly AISystem aiSystem;
    
    // Pathfinding
    private readonly Pathfinding pathfinding;
    private readonly Vector3Int startNodePos;
    private readonly Vector3Int targetNodePos;
    private Stack<Node> path;
    
    // Animation
    private Vector2 movementVector;
    
    // Movement Commands
    private readonly MoveUp up;
    private readonly MoveLeft left;
    private readonly MoveDown down;
    private readonly MoveRight right;

    public MoveState(AISystem system, Pathfinding pathfinding, Vector3Int start, Vector3Int target)
    {
        aiSystem = system;
        this.pathfinding = pathfinding;
        startNodePos = start;
        targetNodePos = target;
        
        up = new MoveUp(system.transform, system.Speed);
        left = new MoveLeft(system.transform, system.Speed);
        down = new MoveDown(system.transform, system.Speed);
        right = new MoveRight(system.transform, system.Speed);
    }

    public IEnumerator Execute()
    {
        path = FindPath(pathfinding);
        
        // Path not found
        if (path == null)
        {
            aiSystem.SetState(new IdleState(aiSystem));
            yield break;
        }
        
        // Path Found -> Move
        while (path.Count > 0)
        {
            MoveTo(path.Peek());
            yield return new WaitForFixedUpdate();
        }
    }

    private Stack<Node> FindPath(Pathfinding pf)
    {
        return pf.FindPath(startNodePos, targetNodePos);
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
        
        movementVector = Vector2.zero;
        
        if (currentPos.x < targetPos.x - .05f)
        {
            right.Execute();
            movementVector += Vector2.right;
        }

        if (currentPos.x > targetPos.x + .05f)
        {
            left.Execute();
            movementVector += Vector2.left;
        }

        if (currentPos.y < targetPos.y - .05f)
        {
            up.Execute();
            movementVector += Vector2.up;
        }

        if (currentPos.y > targetPos.y + .05f)
        {
            down.Execute();
            movementVector += Vector2.down;
        }
        
        // Animation
        Vector2 normalized = movementVector.normalized;
        aiSystem.Animator.SetBool("isWalking", true);
        aiSystem.Animator.SetFloat("Horizontal", normalized.x);
        aiSystem.Animator.SetFloat("Vertical", normalized.y);
    }
}
