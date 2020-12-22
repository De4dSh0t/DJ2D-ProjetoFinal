using System.Collections;
using UnityEngine;

public class RandomPositionState : IState
{
    private readonly AISystem aiSystem;
    private readonly Room room;

    public RandomPositionState(AISystem system, Room currentRoom)
    {
        aiSystem = system;
        room = currentRoom;
    }
    
    public IEnumerator Execute()
    {
        Vector3Int target = ChooseRandomPosition(room);
        aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, target));
        yield break;
    }

    private Vector3Int ChooseRandomPosition(Room r)
    {
        // In case the "GetCurrentRoom" fails and returns a null, this will make the AI stay in its current position
        if (aiSystem.CurrentRoom == null) return aiSystem.PositionInt;
        
        // Randomly chooses a position within the room tilemap
        while (true)
        {
            int rX = Random.Range(r.room.cellBounds.xMin, r.room.cellBounds.xMax);
            int rY = Random.Range(r.room.cellBounds.yMin, r.room.cellBounds.yMax);
            Vector3Int rPos = new Vector3Int(rX, rY, 0);

            if (r.room.HasTile(rPos))
            {
                return rPos;
            }
        }
    }
}
