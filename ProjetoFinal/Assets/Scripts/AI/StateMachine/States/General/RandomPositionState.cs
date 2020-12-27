using System.Collections;
using UnityEngine;

public class RandomPositionState : IState
{
    private readonly AISystem aiSystem;
    private readonly Zone zone;

    public RandomPositionState(AISystem system, Zone currentZone)
    {
        aiSystem = system;
        zone = currentZone;
    }
    
    public IEnumerator Execute()
    {
        Vector3Int target = ChooseRandomPosition(zone);
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, target));
        aiSystem.SetState(new IdleState(aiSystem));
    }

    private Vector3Int ChooseRandomPosition(Zone r)
    {
        // In case the "GetCurrentRoom" fails and returns a null, this will make the AI stay in its current position
        if (aiSystem.CurrentZone == null) return aiSystem.PositionInt;
        
        // Randomly chooses a position within the room tilemap
        while (true)
        {
            int rX = Random.Range(r.ZoneTilemap.cellBounds.xMin, r.ZoneTilemap.cellBounds.xMax);
            int rY = Random.Range(r.ZoneTilemap.cellBounds.yMin, r.ZoneTilemap.cellBounds.yMax);
            Vector3Int rPos = new Vector3Int(rX, rY, 0);

            if (r.ZoneTilemap.HasTile(rPos))
            {
                return rPos;
            }
        }
    }
}
