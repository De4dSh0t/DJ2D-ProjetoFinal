using System.Collections;

public class ScanRoomState : IState
{
    private readonly CleaningAI aiSystem;
    private readonly Zone room;
    private readonly GarbageManager garbageManager;
    private Garbage garbageToCollect;

    public ScanRoomState(CleaningAI system, Zone currentRoom, GarbageManager garbageGen)
    {
        aiSystem = system;
        room = currentRoom;
        garbageManager = garbageGen;
    }

    public IEnumerator Execute()
    {
        ScanGarbage(room);
        
        // No garbage found
        if (garbageToCollect == null)
        {
            aiSystem.SetState(new IdleState(aiSystem));
            aiSystem.GarbageFound = false;
            yield break;
        }
        
        // Garbage found
        aiSystem.SetState(new CleanState(aiSystem, garbageToCollect));
        aiSystem.GarbageFound = true;
    }

    private void ScanGarbage(Zone r)
    {
        foreach (var garbage in garbageManager.SpawnedGarbage)
        {
            // Check if the garbage is in the same room as the AI entity
            if (garbage.Zone.ZoneID == r.ZoneID)
            {
                garbageToCollect = garbage;
                return;
            }
        }

        garbageToCollect = null;
    }
}
