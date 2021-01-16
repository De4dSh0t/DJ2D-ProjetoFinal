using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cleaning : PlayerAction
{
    [Header("Collider Settings")]
    [SerializeField] private LayerMask garbageLayer;
    [SerializeField] private LayerMask garbageCanLayer;
    
    [Header("Currency Settings")]
    [SerializeField] private CurrencyManager currencyManager;
    
    [Header("UI Settings")]
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text nUsesText;
    
    [Header("Garbage Settings")] 
    [SerializeField] private GarbageManager garbageManager;
    
    // Garbage pick-up settings
    private ContactFilter2D garbageFilter;
    private Garbage garbageToPick;
    private float holdingTime;
    private bool hasCleaned;
    
    // Cleanign product settings
    private List<CleaningProduct> availableProducts;
    private CleaningProduct currentProduct;
    private int currentIndex;

    // Garbage discard settings
    private ContactFilter2D garbageCanFilter;
    
    void Start()
    {
        // Get player collider
        pCollider = GetComponent<Collider2D>();
        
        // Get available products
        availableProducts = playerInfo.GetAvailableProducts();
        
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
        
        currentProduct = availableProducts[currentIndex];
        UpdateProductDisplay();
    }
    
    void Update()
    {
        HandleCleaningProduct();
        HandlePickUp();
        HandleDiscard();
    }
    
    private void HandlePickUp()
    {
        if (playerInfo.IsFull) return;
        
        // Tries to get the closest object
        GameObject closestObject = GetClosestObject(garbageFilter);
        if (closestObject == null) return;
        
        // Gets the Garbage component
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Get garbage component from the closestObject variable
            garbageToPick = closestObject.GetComponent<Garbage>();
            
            hasCleaned = false;
        }
        
        // Clean
        if (Input.GetKey(KeyCode.E))
        {
            if (hasCleaned) return;
            
            // Cleaning Time
            holdingTime += Time.deltaTime;
            
            // Check whether the product has enough num of uses or not
            if (!playerInfo.CanUseProduct(currentProduct)) ChangeCurrentProduct();
            
            if (holdingTime < Mathf.Clamp(garbageToPick.CleaningTime - currentProduct.TimeBoost, 0, garbageToPick.CleaningTime)) return;
            
            Clean(garbageToPick);
            
            holdingTime = 0;
            hasCleaned = true;
        }
        
        // Reset holding time
        if (Input.GetKeyUp(KeyCode.E)) holdingTime = 0;
    }
    
    private void Clean(Garbage garbage)
    {
        // Removes garbage from garbage manager
        garbageManager.RemoveGarbage(garbage);
        
        // Destroy gameObject
        Destroy(garbage.gameObject);
        
        // Update num of uses for the cleaning product
        playerInfo.UpdateProductNUses(currentProduct, -1);
        
        // Update the carrying count
        playerInfo.CarryingCount++;
        
        // Update currency
        currencyManager.UpdateCurrency(garbage.CleaningReward);
        
        // Update product in hand (Image and Count)
        UpdateProductDisplay();
        
        if (!playerInfo.CanUseProduct(currentProduct)) ChangeCurrentProduct();
    }

    private void HandleCleaningProduct()
    {
        if (currentProduct == null) currentProduct = availableProducts[currentIndex];

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeCurrentProduct();
        }
    }

    private void ChangeCurrentProduct()
    {
        // Update List
        availableProducts = playerInfo.GetAvailableProducts();
        
        int maxIndex = availableProducts.Count - 1;
        
        currentIndex++;
        if (currentIndex > maxIndex) currentIndex = 0;

        currentProduct = availableProducts[currentIndex];
        
        // Update product in hand (Image and Count)
        UpdateProductDisplay();
    }
    
    private void UpdateProductDisplay()
    {
        productImage.sprite = currentProduct.Sprite;
        nUsesText.text = playerInfo.GetNumUses(currentProduct).ToString();
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
        
        // Reset variable
        garbageToPick = null;
    }
}