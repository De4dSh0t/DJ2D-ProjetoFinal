using System.Collections.Generic;
using UnityEngine;

public class GuestAI : AISystem
{
    // Food Settings
    private Zone restaurant;
    
    // Decision Settings
    private int sIndex;
    private bool hasOrdered;
    private Vector3Int pickUpPos;
    
    // Garbage Detection Settings
    [Header("Garbage Detection Settings")] 
    [SerializeField] private LayerMask garbageLayer;
    private HashSet<GameObject> encounteredGarbage;
    private ContactFilter2D garbageFilter;
    private Collider2D gCollider;

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
        
        // Garbage Detection
        // Using hashset to avoid gameObject repetitions
        encounteredGarbage = new HashSet<GameObject>();
        gCollider = GetComponent<Collider2D>();
        garbageFilter = new ContactFilter2D
        {
            layerMask = garbageLayer,
            useLayerMask = true,
            useTriggers = true
        };
        
        DecisionMaking();
    }

    void Update()
    {
        GarbageDetection();
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
    
    /// <summary>
    /// Detects garbage when in range and saves in a hashset
    /// </summary>
    private void GarbageDetection()
    {
        // Save all the colliders that the entity is currently colliding with
        List<Collider2D> contacts = new List<Collider2D>();
        gCollider.OverlapCollider(garbageFilter, contacts);
        
        // Returns null if no collider has been detected
        if (contacts.Count <= 0) return;
        
        // Fills the hashset with the garbage gameObject
        foreach (var garbage in contacts)
        {
            encounteredGarbage.Add(garbage.gameObject);
        }
    }
}