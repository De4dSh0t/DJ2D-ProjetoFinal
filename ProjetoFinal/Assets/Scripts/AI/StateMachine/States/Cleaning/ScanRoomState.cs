using System.Collections;

public class ScanRoomState : IState
{
    private readonly CleaningAI aiSystem;
    private readonly Room room;
    private readonly GarbageGenerator garbageGenerator;
    private Garbage garbageToCollect;

    public ScanRoomState(CleaningAI system, Room currentRoom, GarbageGenerator garbageGen)
    {
        aiSystem = system;
        room = currentRoom;
        garbageGenerator = garbageGen;
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

    private void ScanGarbage(Room r)
    {
        foreach (var garbage in garbageGenerator.spawnedGarbage)
        {
            // Check if the garbage is in the same room as the AI entity
            if (garbage.room.RoomID == r.RoomID)
            {
                garbageToCollect = garbage;
                return;
            }
        }

        garbageToCollect = null;
    }
}
