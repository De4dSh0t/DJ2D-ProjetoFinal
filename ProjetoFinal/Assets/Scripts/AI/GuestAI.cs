using UnityEngine;

public class GuestAI : AISystem
{
    private int sIndex;
    private Room restaurant;
    
    void Start()
    {
        // Finds the restaurant room reference
        restaurant = SearchRoom("Restaurant");
        
        DecisionMaking();
    }
    
    public override void DecisionMaking()
    {
        switch (sIndex)
        {
            case 0: // Random Movement
            {
                SetState(new RandomPositionState(this, CurrentRoom));
                break;
            }
            case 1: // Change Room
            {
                SetState(new ChangeRoomState(this, rooms[Random.Range(0, rooms.Length)]));
                break;
            }
            case 2: // Pick prepared order
            {
                SetState(new MoveState(this, Pathfinding, PositionInt, restaurant.deliverWaypoint));
                break;
            }
        }
    }

    /// <summary>
    /// Notifies the guest AI when order has been prepared
    /// </summary>
    public void PickUpOrder()
    {
        sIndex = 2;
    }
}
