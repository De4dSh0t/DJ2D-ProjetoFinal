using System;
using TMPro;
using UnityEngine;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text numUses;
    [SerializeField] private TMP_Text price;
    private CleaningProduct cleaningProduct;
    
    public event Action<CleaningProduct> OnSelect; 
    
    public void Setup(CleaningProduct product)
    {
        title.text = product.ProductID;
        numUses.text = product.NumberOfUses.ToString();
        price.text = product.Cost.ToString();
        
        cleaningProduct = product;
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke(cleaningProduct);
    }
}