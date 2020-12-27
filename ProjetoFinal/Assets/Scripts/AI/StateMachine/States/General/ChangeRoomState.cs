using System.Collections;
using UnityEngine;

public class ChangeRoomState : IState
{
    private readonly AISystem aiSystem;
    private readonly Zone targetRoom;
    private Vector3Int targetWaypoint;

    public ChangeRoomState(AISystem system, Zone target)
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
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, targetWaypoint));
        aiSystem.SetState(new IdleState(aiSystem));
    }

    private bool CurrentRoomIsEqual(Zone target)
    {
        return aiSystem.CurrentZone == target;
    }

    private Vector3Int GetEntryWaypoint(Zone target)
    {
        return target.EntryWaypoint;
    }
}
