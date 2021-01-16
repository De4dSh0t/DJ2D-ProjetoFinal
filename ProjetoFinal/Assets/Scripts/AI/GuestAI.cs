﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestAI : AISystem
{
    // Food Settings
    private Zone restaurant;
    private int maxNumOfOrders = 2;
    private int numOfOrders;
    
    // Decision Settings
    [Header("Decision Settings")]
    [SerializeField] [Tooltip("X: Min | Y: Max")] private Vector2Int movementsToChangeRoom;
    [SerializeField] [Tooltip("X: Min | Y: Max")] private Vector2 spawnRate;
    private Vector3Int pickUpPos;
    private bool isSpawningGarbage;
    private int nAllowedMovements;
    private int movementsInRoom;
    private bool hasOrdered;
    private int sIndex;
    
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
        
        // Determine num of allowed movements in the same room (to control when to change room)
        nAllowedMovements = Random.Range(movementsToChangeRoom.x, movementsToChangeRoom.y);
        
        DecisionMaking();
        
        // Start random garbage spawn coroutine
        StartCoroutine(RandomGarbageSpawn());
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
                movementsInRoom++;
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
                isSpawningGarbage = false;
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
        if (CurrentZone == restaurant && !hasOrdered && numOfOrders < maxNumOfOrders)
        {
            sIndex = 2;
            hasOrdered = true;
            numOfOrders++;
            return;
        }
        
        // Spawn garbage once the entity eats
        if (HasEaten)
        {
            sIndex = 4;
            HasEaten = false;
            hasOrdered = false;
            return;
        }
        
        // Wait in restaurant until the entity eats
        if (hasOrdered && !HasEaten) return;
        
        // Checks whether the entity is going to spawn garbage or not
        if (isSpawningGarbage && movementsInRoom != 0)
        {
            sIndex = 4;
            return;
        }
        
        // Controls when to change room
        if (movementsInRoom >= nAllowedMovements)
        {
            sIndex = 1;
            movementsInRoom = 0;
            nAllowedMovements = Random.Range(movementsToChangeRoom.x, movementsToChangeRoom.y);
            return;
        }
        
        sIndex = 0;
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
    
    private IEnumerator RandomGarbageSpawn()
    {
        float rate = Random.Range(spawnRate.x, spawnRate.y);
        
        while (true)
        {
            if (hasOrdered && !HasEaten) yield return null;
            if (isSpawningGarbage) yield return null;
            
            yield return new WaitForSeconds(rate);
            isSpawningGarbage = true;
            rate = Random.Range(spawnRate.x, spawnRate.y);
        }
    }
}