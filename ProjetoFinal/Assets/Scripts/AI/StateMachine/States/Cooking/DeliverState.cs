using System.Collections;
using UnityEngine;

public class DeliverState : IState
{
    private readonly CookingAI aiSystem;
    private readonly GuestAI guestAI;
    private readonly Room room;

    public DeliverState(CookingAI system, GuestAI guest, Room currentRoom)
    {
        aiSystem = system;
        guestAI = guest;
        room = currentRoom;
    }

    public IEnumerator Execute()
    {
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, room.deliverWaypoint));
        Debug.Log("Food Delivered.");
        aiSystem.IsCooking = false;
        
        // Notifies the guest that his order has been delivered
        guestAI.PickUpOrder();
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}
