using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GuestAI : AISystem
{
    [Header("Food Settings")]
    [SerializeField] private List<Food> foodList;
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
    /// Returns the list of available food to order
    /// </summary>
    public List<Food> FoodList => foodList;

    public bool HasEaten { get; set; }

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
                ActionZone seat = restaurant.AvailableActionZone;
                SetState(new EatState(this, pickUpPos, seat));
                break;
            }
        }
    }

    /// <summary>
    /// Notifies the guest AI when the order has been prepared
    /// </summary>
    public void PickUpOrder(Vector3Int deliverPos)
    {
        sIndex = 3;
        Tilemap currentZone = CurrentZone.ZoneTilemap;
        
        // Determine Pick-Up position
        if (currentZone.HasTile(deliverPos + new Vector3Int(-2, 0, 0)))
        {
            pickUpPos = deliverPos + new Vector3Int(-2, 0, 0);
            return;
        }
        if (currentZone.HasTile(deliverPos + new Vector3Int(2, 0, 0)))
        {
            pickUpPos = deliverPos + new Vector3Int(2, 0, 0);
            return;
        }
        if (currentZone.HasTile(deliverPos + new Vector3Int(0, 2, 0)))
        {
            pickUpPos = deliverPos + new Vector3Int(0, 2, 0);
            return;
        }
        if (currentZone.HasTile(deliverPos + new Vector3Int(0, -2, 0)))
        {
            pickUpPos = deliverPos + new Vector3Int(0, -2, 0);
            return;
        }
        
        print("Delivery not found!");
    }

    private void HandleStates()
    {
        // Check if the entity is in the restaurant and if it hasn't already odered food
        if (CurrentZone == restaurant && !hasOrdered)
        {
            sIndex = 2;
            hasOrdered = true;
        }

        // Once the entity has eaten, it should go to another room
        if (HasEaten)
        {
            sIndex = 1;
            HasEaten = false;
        }
    }
}