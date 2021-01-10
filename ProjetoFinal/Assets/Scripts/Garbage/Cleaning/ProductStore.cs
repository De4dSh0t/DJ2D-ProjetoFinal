using System.Collections.Generic;
using UnityEngine;

public class ProductStore : MonoBehaviour
{
    [Header("Products Settings")]
    [SerializeField] private List<CleaningProduct> cleaningProducts;
    
    [Header("Prefab Settings")]
    [SerializeField] private GameObject productPrefab;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject content;

    private void Start()
    {
        DisplayList();
    }

    private void DisplayList()
    {
        foreach (var product in cleaningProducts)
        {
            GameObject pButton = Instantiate(productPrefab, content.transform);
            pButton.GetComponent<ProductButton>().Setup(product.ProductID, product.NumberOfUses, product.Cost);
        }
    }
}