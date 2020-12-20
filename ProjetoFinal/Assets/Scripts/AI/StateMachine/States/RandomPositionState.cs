using System.Collections;
using UnityEngine;

public class RandomPositionState : IState
{
    private readonly AISystem aiSystem;
    private Room room;

    public RandomPositionState(AISystem system)
    {
        aiSystem = system;
    }
    
    public IEnumerator Execute()
    {
        room = GetCurrentRoom(aiSystem.PositionInt);
        Vector3Int target = ChooseRandomPosition(room);
        aiSystem.SetState(new FindState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, target));
        yield break;
    }

    private Room GetCurrentRoom(Vector3Int currentPos)
    {
        foreach (var r in aiSystem.rooms)
        {
            if (r.room.HasTile(currentPos)) return r;
        }
        
        return null;
    }

    private Vector3Int ChooseRandomPosition(Room r)
    {
        // In case the "GetCurrentRoom" fails and returns a null, this will make the AI stay in its current position
        if (room == null) return aiSystem.PositionInt;
        
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
