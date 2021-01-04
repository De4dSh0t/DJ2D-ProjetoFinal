using UnityEngine;

public class GuestAI : AISystem
{
    // Food Settings
    private Zone restaurant;
    
    // Decision Settings
    private int sIndex;
    private bool hasOrdered;
    private Vector3Int pickUpPos;

    /// <summary>
    /// Returns OrderManager reference to interact with the order list
    /// </summary>
    public OrderManager OrderManager { get; private set; }
    
    /// <summary>
    /// Returns GarbageManager reference
    /// </summary>
    public GarbageManager GarbageManager { get; private set; }
    
    public bool HasEaten { get; set; }

    void Start()
    {
        OrderManager = FindObjectOfType<OrderManager>();
        GarbageManager = FindObjectOfType<GarbageManager>();
        restaurant = SearchZone("Restaurant");
        
        DecisionMaking();
    }
    
    public override void DecisionMaking()
    {
        HandleStates();

        switch (sIndex)
        {
            case 0: // Random Position
            {
                SetState(new RandomPositionState(this, CurrentZone));
                break;
            }
            case 1: // Change Room
            {
                SetState(new ChangeRoomState(this, zones[Random.Range(0, zones.Length)]));
                break;
            }
            case 2: // Order Food (goto Random Position)
            {
                SetState(new OrderState(this));
                sIndex = 0;
                break;
            }
            case 3: // Pick Food
            {
                SetState(new EatState(this, SearchActionZone("PickUp1").Waypoint, restaurant.AvailableActionZone));
                break;
            }
            case 4: // Spawn Garbage (goto Change Room)
            {
                SetState(new SpawnGarbageState(this));
                sIndex = 1;
                break;
            }
        }
    }

    /// <summary>
    /// Notifies the guest AI when the order has been prepared
    /// </summary>
    public void PickUpOrder()
    {
        sIndex = 3;
    }

    private void HandleStates()
    {
        // Check if the entity is in the restaurant and if it hasn't already ordered food
        if (CurrentZone == restaurant && !hasOrdered)
        {
            sIndex = 2;
            hasOrdered = true;
        }

        // Once the entity has eaten, it should go to another room
        if (HasEaten)
        {
            sIndex = 4;
            HasEaten = false;
        }
    }
}