using System.Collections;

public class QuitState : IState
{
    private readonly GuestAI aiSystem;
    private readonly Zone exitZone;
    
    public QuitState(GuestAI system, Zone innEntry)
    {
        aiSystem = system;
        exitZone = innEntry;
    }
    
    public IEnumerator Execute()
    {
        yield return aiSystem.SetState(new ChangeRoomState(aiSystem, exitZone));
        
        // Destoy guest
        aiSystem.DestroyCleaner();
    }
}
