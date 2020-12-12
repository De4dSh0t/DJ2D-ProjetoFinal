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
    private bool pathFound;
    
    public FindState(AISystem system, Pathfinding pathfinding, Vector3Int start, Vector3Int target)
    {
        aiSystem = system;
        this.pathfinding = pathfinding;
        startPos = start;
        targetPos = target;

        this.pathfinding.PathFound += PathFound;
    }
    
    public IEnumerator Execute()
    {
        FindPath(pathfinding);
        yield return new WaitUntil(() => pathFound);
        aiSystem.SetState(new MoveState(aiSystem, path));
    }

    private void FindPath(Pathfinding pathfinding)
    {
        pathfinding.FindPath(startPos, targetPos);
    }

    private void PathFound(Stack<Node> path)
    {
        pathFound = true;
        this.path = path;
    }
}
