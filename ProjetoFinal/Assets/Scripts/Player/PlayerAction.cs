using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Collider Settings")]
    [SerializeField] private LayerMask contactLayer;
    private Collider2D pCollider;
    
    [Header("Player Info Settings")]
    [SerializeField] private PlayerInfo playerInfo;
    private Garbage garbageToPick;
    
    [Header("Discard Settings")]
    [SerializeField] private Zone garbageRoom;
    
    // Pick-Up Settings
    private float holdingTime;
    private CleaningProduct requiredProduct;
    private bool hasCleaned;
    
    void Start()
    {
        pCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        GetClosestGarbage();
        HandlePickUp();
    }
    
    private void HandlePickUp()
    {
        if (playerInfo.IsFull) return;
        
        // Tries to get the closest garbage
        garbageToPick = GetClosestGarbage();
        if (garbageToPick == null) return;
        
        // Check if contains product to clean
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if player contains required cleaning product
            requiredProduct = garbageToPick.RequiredProduct;
            if (playerInfo.GetProduct(requiredProduct.ProductID) == null)
            {
                print("Player doesn't contain the required cleaning product!");
                return;
            }
            
            // Prevents the player from "cleaning" without first checking if contains the required cleaning product
            hasCleaned = false;
        }
        
        // Clean
        if (Input.GetKey(KeyCode.E))
        {
            if (hasCleaned) return;
            
            // Cleaning Time
            holdingTime += Time.deltaTime;
            if (holdingTime < garbageToPick.CleaningTime) return;
            
            Clean(garbageToPick);
            
            hasCleaned = true;
        }
        
        // Reset holding time
        if (Input.GetKeyUp(KeyCode.E)) holdingTime = 0;
    }
    
    private Garbage GetClosestGarbage()
    {
        // Configure filter to ignore other collisions (except with the "contactLayer")
        ContactFilter2D filter = new ContactFilter2D
        {
            layerMask = contactLayer,
            useLayerMask = true,
            useTriggers = true
        };
        
        // Save all the colliders that the entity is currently colliding with
        List<Collider2D> contacts = new List<Collider2D>();
        pCollider.OverlapCollider(filter, contacts);
        
        // Returns null if no collider has been detected
        if (contacts.Count <= 0) return null;
        
        // This is used as a reference to calculate the closest object
        int iClosest = 0;
        ColliderDistance2D closest = contacts[0].Distance(pCollider);
        
        // Calculates the closest object
        for (int i = 0; i < contacts.Count; i++)
        {
            ColliderDistance2D temp = contacts[i].Distance(pCollider);
            
            if (temp.distance < closest.distance)
            {
                closest = temp;
                iClosest = i;
            }
        }
        
        // Returns the closest garbage object
        return contacts[iClosest].GetComponent<Garbage>();
    }
    
    private void Clean(Garbage garbage)
    {
        // Destroy gameObject
        Destroy(garbage.gameObject);
        
        // Update available products to removed the product that has been used to clean
        if (playerInfo.AvailableProducts.ContainsKey(requiredProduct)) playerInfo.AvailableProducts[requiredProduct]--;
        
        // Update the carrying count
        playerInfo.CarryingCount++;
    }
}
