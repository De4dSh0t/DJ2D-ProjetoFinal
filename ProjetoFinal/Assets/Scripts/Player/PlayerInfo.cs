using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Carrying Settings")]
    [SerializeField] private int maxCarryingCapacity;
    private int carryingCount;
    
    [Header("Cleaning Products Settings")]
    [SerializeField] private CleaningProduct[] cleaningProducts;
    
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
            
            carryingCount += value;
        }
    }
    
    /// <summary>
    /// Returns a boolean depending on the carrying capacity and its maximum
    /// </summary>
    public bool IsFull => carryingCount >= maxCarryingCapacity;

    /// <summary>
    /// Returns a dictionary of available products and the remaining num of uses
    /// </summary>
    public Dictionary<CleaningProduct, int> AvailableProducts { get; private set; }
    
    void Start()
    {
        // Generate available products dictionary
        AvailableProducts = new Dictionary<CleaningProduct, int>();
        foreach (var product in cleaningProducts)
        {
            AvailableProducts.Add(product, product.NumberOfUses);
        }
    }
    
    void Update()
    {
        
    }
    
    /// <summary>
    /// Returns a product if in "inventory" (otherwise, returns null)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public CleaningProduct GetProduct(string id)
    {
        foreach (var product in AvailableProducts.Keys)
        {
            // Remove product if number of uses reaches zero
            if (AvailableProducts[product] <= 0)
            {
                AvailableProducts.Remove(product);
                break;
            }
            
            // Return specified product
            if (product.ProductID == id) return product;
        }
        
        return null;
    }
}