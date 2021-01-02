using System.Collections;
using UnityEngine;

public class DeliverState : IState
{
    private readonly CookingAI aiSystem;
    private readonly GuestAI guestAI;
    private readonly Food food;
    private readonly Zone zone;

    public DeliverState(CookingAI system, GuestAI guest, Food foodToDeliver, Zone currentZone)
    {
        aiSystem = system;
        guestAI = guest;
        food = foodToDeliver;
        zone = currentZone;
    }

    public IEnumerator Execute()
    {
        // Go to delivery pos
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, aiSystem.SearchActionZone("Deliver").Waypoint));
        Debug.Log("Food Delivered.");
        aiSystem.IsCooking = false;
        
        // Notifies the guest that his order has been delivered
        guestAI.PickUpOrder();
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}
