using System.Collections;
using UnityEngine;

public class EatState : IState
{
    private readonly GuestAI aiSystem;
    private readonly Vector3Int foodPos;
    private readonly Vector3Int seatPos;

    public EatState(GuestAI system, Vector3Int foodPosition, Vector3Int seatPosition)
    {
        aiSystem = system;
        foodPos = foodPosition;
        seatPos = seatPosition;
    }

    public IEnumerator Execute()
    {
        // Pick up food
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, foodPos));
        
        // Sit
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, seatPos));
        
        // Eat
        yield return new WaitForSeconds(10); // Eating time
        aiSystem.HasEaten = true;
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}
