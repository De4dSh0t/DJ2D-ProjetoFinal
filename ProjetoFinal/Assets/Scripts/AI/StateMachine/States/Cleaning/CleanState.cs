using System.Collections;

public class CleanState : IState
{
    private readonly CleaningAI aiSystem;
    private Garbage garbageToCollect;

    public CleanState(CleaningAI system, Garbage garbage)
    {
        aiSystem = system;
        garbageToCollect = garbage;
    }

    public IEnumerator Execute()
    {
        IState move = new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, garbageToCollect.position);
        yield return aiSystem.StartCoroutine(move.Execute()); // Waits for the AI entity to reach its destination
        
        Clean();
        aiSystem.SetState(new IdleState(aiSystem));
    }

    private void Clean()
    {
        aiSystem.PickUp(garbageToCollect);
    }
}
