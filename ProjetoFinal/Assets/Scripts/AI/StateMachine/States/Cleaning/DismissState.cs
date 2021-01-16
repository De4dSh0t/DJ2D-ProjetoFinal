using System.Collections;

public class DismissState : IState
{
    private readonly CleaningAI aiSystem;
    private readonly Zone exitZone;
    
    public DismissState(CleaningAI system, Zone innEntry)
    {
        aiSystem = system;
        exitZone = innEntry;
    }
    
    public IEnumerator Execute()
    {
        yield return aiSystem.SetState(new ChangeRoomState(aiSystem, exitZone));
        
        // Destoy cleaner
        aiSystem.DestroyCleaner();
    }
}