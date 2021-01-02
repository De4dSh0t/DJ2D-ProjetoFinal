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
        Vector3Int deliveryPos = aiSystem.SearchActionZone("Deliver").Waypoint;
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, deliveryPos));
        Debug.Log("Food Delivered.");
        aiSystem.IsCooking = false;
        
        // Notifies the guest that his order has been delivered
        guestAI.PickUpOrder(deliveryPos);
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}
