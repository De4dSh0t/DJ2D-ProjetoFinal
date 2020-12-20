using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindState : IState
{
    private readonly AISystem aiSystem;
    private readonly Pathfinding pathfinding;
    private readonly Vector3Int startPos;
    private readonly Vector3Int targetPos;
    private Stack<Node> path;

    public FindState(AISystem system, Pathfinding pathfinding, Vector3Int start, Vector3Int target)
    {
        aiSystem = system;
        this.pathfinding = pathfinding;
        startPos = start;
        targetPos = target;
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
        
        // Path found
        aiSystem.SetState(new MoveState(aiSystem, path));
    }

    private Stack<Node> FindPath(Pathfinding pf)
    {
        return pf.FindPath(startPos, targetPos);
    }
}
