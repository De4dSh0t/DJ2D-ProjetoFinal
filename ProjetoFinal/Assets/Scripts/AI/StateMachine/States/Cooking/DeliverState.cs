using System.Collections;
using UnityEngine;

public class DeliverState : IState
{
    private readonly CookingAI aiSystem;
    private readonly GuestAI guestAI;
    private readonly Zone zone;

    public DeliverState(CookingAI system, GuestAI guest, Zone currentZone)
    {
        aiSystem = system;
        guestAI = guest;
        zone = currentZone;
    }

    public IEnumerator Execute()
    {
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, zone.ActionWaypoint));
        Debug.Log("Food Delivered.");
        aiSystem.IsCooking = false;
        
        // Notifies the guest that his order has been delivered
        guestAI.PickUpOrder();
        
        aiSystem.SetState(new IdleState(aiSystem));
    }
}
