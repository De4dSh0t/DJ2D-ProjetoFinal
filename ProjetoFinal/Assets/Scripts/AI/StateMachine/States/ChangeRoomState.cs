using System.Collections;
using UnityEngine;

public class ChangeRoomState : IState
{
    private readonly AISystem aiSystem;
    private readonly Room targetRoom;
    private Vector3Int targetWaypoint;

    public ChangeRoomState(AISystem system, Room target)
    {
        aiSystem = system;
        targetRoom = target;
    }

    public IEnumerator Execute()
    {
        // In case the AI is in the same room as the target room, then it won't move
        if (CurrentRoomIsEqual(targetRoom))
        {
            aiSystem.SetState(new IdleState(aiSystem));
            yield break;
        }
        
        Debug.Log("Change Room");
        
        targetWaypoint = GetEntryWaypoint(targetRoom);
        aiSystem.SetState(new FindState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, targetWaypoint));
    }

    private bool CurrentRoomIsEqual(Room target)
    {
        return aiSystem.CurrentRoom == target;
    }

    private Vector3Int GetEntryWaypoint(Room target)
    {
        return target.entryWaypoint;
    }
}
