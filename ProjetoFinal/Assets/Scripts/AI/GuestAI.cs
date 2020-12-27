using System.Collections.Generic;
using UnityEngine;

public class GuestAI : AISystem
{
    [Header("Food Settings")]
    [SerializeField] private List<Food> foodList;
    private Zone restaurant;
    
    // Decision Settings
    private int sIndex;
    private bool hasOrdered;

    /// <summary>
    /// Returns OrderManager reference to interact with the order list
    /// </summary>
    public OrderManager OrderManager { get; private set; }

    /// <summary>
    /// Returns the list of available food to order
    /// </summary>
    public List<Food> FoodList => foodList;

    void Start()
    {
        OrderManager = FindObjectOfType<OrderManager>();
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
            case 3: // Pick Food (goto Random Position)
            {
                SetState(new MoveState(this, Pathfinding, PositionInt, restaurant.ActionWaypoint));
                sIndex = 0;
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
        // Check if the entity is in the restaurant and if it hasn't already odered food
        if (CurrentZone == restaurant && !hasOrdered)
        {
            sIndex = 2;
            hasOrdered = true;
        }
    }
}
