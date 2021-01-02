using System.Collections;
using UnityEngine;

public class EatState : IState
{
    private readonly GuestAI aiSystem;
    private readonly Vector3Int foodPos;
    private ActionZone seat;

    public EatState(GuestAI system, Vector3Int foodPosition, ActionZone seatZone)
    {
        aiSystem = system;
        foodPos = foodPosition;
        seat = seatZone;
    }

    public IEnumerator Execute()
    {
        // Pick up food
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, foodPos));

        yield return new WaitForSeconds(1);
        
        // Sit
        seat.InUse = true;
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, seat.Waypoint));

        // Eat
        yield return new WaitForSeconds(10); // Eating time
        aiSystem.HasEaten = true;
        seat.InUse = false;
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}