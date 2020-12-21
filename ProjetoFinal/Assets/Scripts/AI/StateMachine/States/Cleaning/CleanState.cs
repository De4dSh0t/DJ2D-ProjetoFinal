using System.Collections;
using UnityEngine;

public class CleanState : IState
{
    private readonly AISystem aiSystem;
    private Garbage garbageToCollect;

    public CleanState(AISystem system, Garbage garbage)
    {
        aiSystem = system;
        garbageToCollect = garbage;
    }

    public IEnumerator Execute()
    {
        IState move = new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, garbageToCollect.position);
        yield return aiSystem.StartCoroutine(move.Execute()); // Waits for the AI entity to reach its destination
        Clean();
    }

    private void Clean()
    {
        Debug.Log("Cleaned!");
    }
}
