using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private Image productImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text numUses;
    [SerializeField] private TMP_Text boost;
    [SerializeField] private TMP_Text price;
    private CleaningProduct cleaningProduct;
    
    public event Action<CleaningProduct> OnSelect; 
    
    public void Setup(CleaningProduct product)
    {
        productImage.sprite = product.Sprite;
        title.text = product.ProductID;
        numUses.text = product.NumberOfUses.ToString();
        boost.text = product.TimeBoost.ToString();
        price.text = product.Cost.ToString();
        
        cleaningProduct = product;
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke(cleaningProduct);
    }
}