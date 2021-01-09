using UnityEngine;

public class Cleaning : PlayerAction
{
    [Header("Collider Settings")]
    [SerializeField] private LayerMask garbageLayer;
    [SerializeField] private LayerMask garbageCanLayer;
    
    [Header("Currency Settings")]
    [SerializeField] private CurrencyManager currencyManager;
    
    // Garbage pick-up settings
    private ContactFilter2D garbageFilter;
    private CleaningProduct requiredProduct;
    private Garbage garbageToPick;
    private float holdingTime;
    private bool hasCleaned;
    
    // Garbage discard settings
    private ContactFilter2D garbageCanFilter;
    
    void Start()
    {
        // Get player collider
        pCollider = GetComponent<Collider2D>();
        
        // Garbage Filter
        // Configure filter to ignore other collisions (except with the "contactLayer")
        garbageFilter = new ContactFilter2D
        {
            layerMask = garbageLayer,
            useLayerMask = true,
            useTriggers = true
        };
        
        // Garbage Can Filter
        garbageCanFilter = new ContactFilter2D
        {
            layerMask = garbageCanLayer,
            useLayerMask = true,
            useTriggers = true
        };
    }
    
    void Update()
    {
        HandlePickUp();
        HandleDiscard();
    }
    
    private void HandlePickUp()
    {
        if (playerInfo.IsFull) return;
        
        // Tries to get the closest object
        GameObject closestObject = GetClosestObject(garbageFilter);
        if (closestObject == null) return;
        
        // Check if contains product to clean
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get garbage component from the closestObject variable
            garbageToPick = closestObject.GetComponent<Garbage>();
            
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
    
    private void Clean(Garbage garbage)
    {
        // Destroy gameObject
        Destroy(garbage.gameObject);
        
        // Update available products to removed the product that has been used to clean
        if (playerInfo.AvailableProducts.ContainsKey(requiredProduct)) playerInfo.AvailableProducts[requiredProduct]--;
        
        // Update the carrying count
        playerInfo.CarryingCount++;
        
        // Update currency
        currencyManager.UpdateCurrency(garbage.CleaningReward);
    }
    
    private void HandleDiscard()
    {
        // Checks if player has any garbage to discard
        if (playerInfo.CarryingCount <= 0) return;
        
        // Tries to get a garbage can gameObject
        GameObject closestObject = GetClosestObject(garbageCanFilter);
        if (closestObject == null) return;
        
        // Discards garbage
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Reset carrying count
            playerInfo.CarryingCount = 0;
            print("Garbage discarded!");
        }
    }
}