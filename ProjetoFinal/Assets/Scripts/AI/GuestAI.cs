using System.Collections.Generic;
using UnityEngine;

public class GuestAI : AISystem
{
    [Header("Food Settings")]
    [SerializeField] private List<Food> foodList;
    private Room restaurant;
    
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
        restaurant = SearchRoom("Restaurant");
        
        DecisionMaking();
    }
    
    public override void DecisionMaking()
    {
        HandleFoodOrder();

        switch (sIndex)
        {
            case 0: // Random Position
            {
                SetState(new RandomPositionState(this, CurrentRoom));
                break;
            }
            case 1: // Change Room
            {
                SetState(new ChangeRoomState(this, rooms[Random.Range(0, rooms.Length)]));
                break;
            }
            case 2: // Order Food
            {
                SetState(new OrderState(this));
                break;
            }
            case 3: // Pick Food
            {
                SetState(new MoveState(this, Pathfinding, PositionInt, restaurant.deliverWaypoint));
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

    private void HandleFoodOrder()
    {
        // Check if the entity is in the restaurant and if it hasn't already odered food
        if (CurrentRoom == restaurant && !hasOrdered)
        {
            sIndex = 2;
            hasOrdered = true;
        }
    }
}
