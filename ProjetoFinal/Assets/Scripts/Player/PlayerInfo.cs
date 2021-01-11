using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Carrying Settings")]
    [SerializeField] private int maxCarryingCapacity;
    private int carryingCount;
    
    [Header("Cleaning Products Settings")]
    [SerializeField] private CleaningProduct[] cleaningProducts;
    private Dictionary<CleaningProduct, int> availableProducts = new Dictionary<CleaningProduct, int>();
    
    /// <summary>
    /// Current carrying count
    /// </summary>
    public int CarryingCount
    {
        get => carryingCount;
        set
        {
            if (carryingCount + value >= maxCarryingCapacity)
            {
                carryingCount = maxCarryingCapacity;
                print("Cannot carry more garbage!");
                return;
            }
            
            carryingCount = value;
        }
    }
    
    /// <summary>
    /// Returns a boolean depending on the carrying capacity and its maximum
    /// </summary>
    public bool IsFull => carryingCount >= maxCarryingCapacity;
    
    void Awake()
    {
        // Generate available products dictionary
        availableProducts = new Dictionary<CleaningProduct, int>();
        foreach (var product in cleaningProducts)
        {
            availableProducts.Add(product, product.NumberOfUses);
        }
    }
    
    public List<CleaningProduct> GetAvailableProducts()
    {
        List<CleaningProduct> products = new List<CleaningProduct>();

        foreach (var product in availableProducts.Keys)
        {
            products.Add(product);
        }

        return products;
    }
    
    public int GetNumUses(CleaningProduct product)
    {
        if (availableProducts[product] <= 0)
        {
            availableProducts.Remove(product);
            return 0;
        }
        
        return availableProducts[product];
    }
    
    public bool CanUseProduct(CleaningProduct product)
    {
        if (!availableProducts.ContainsKey(product)) return false;
        
        if (availableProducts[product] <= 0)
        {
            availableProducts.Remove(product);
            return false;
        }
        
        return true;
    }

    public void UpdateProductNUses(CleaningProduct product, int value)
    {
        // Check if the product has unlimited num uses or not
        if (product.Unlimited) return;
        
        int temp = availableProducts[product] + value;
        if (temp <= 0) availableProducts[product] = 0;
        else availableProducts[product] += value;
    }
}