using System.Collections;

public class CleanState : IState
{
    private readonly CleaningAI aiSystem;
    private readonly Garbage garbageToCollect;

    public CleanState(CleaningAI system, Garbage garbage)
    {
        aiSystem = system;
        garbageToCollect = garbage;
    }

    public IEnumerator Execute()
    {
        // Waits for the AI entity to reach its destination
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, garbageToCollect.position));
        
        Clean();
        //aiSystem.SetState(new IdleState(aiSystem));
    }

    private void Clean()
    {
        aiSystem.PickUp(garbageToCollect);
    }
}
