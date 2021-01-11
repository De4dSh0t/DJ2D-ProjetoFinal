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
    
    /// <summary>
    /// Returns a product if in "inventory" (otherwise, returns null)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CleaningProduct GetProduct(string id)
    {
        foreach (var product in availableProducts.Keys)
        {
            // Remove product if number of uses reaches zero
            if (availableProducts[product] <= 0)
            {
                availableProducts.Remove(product);
                break;
            }
            
            // Return specified product
            if (product.ProductID == id) return product;
        }
        
        return null;
    }

    public bool CanUseProduct(CleaningProduct product)
    {
        return availableProducts[product] > 0;
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