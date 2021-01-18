using Audio;
using UnityEngine;
using UnityEngine.UI;

public class ProductMenu : Menu<CleaningProduct>
{
    [Header("Player Settings")]
    [SerializeField] private PlayerInfo playerInfo;
    private CleaningProduct selectedProduct;
    
    protected override void DisplayList()
    {
        foreach (var product in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            ProductButton pButton = button.GetComponent<ProductButton>();
            
            pButton.Setup(product);
            pButton.OnSelect += ShowPrompt;
            spawnedButtons.Add(button);
            
            // Disables button interaction if player doesn't have enough money to buy
            if (product.Cost > currencyManager.CurrentCurrency) pButton.GetComponent<Button>().interactable = false;
        }
    }
    
    protected override void ShowPrompt(CleaningProduct product)
    {
        prompt.SetActive(true);
        selectedProduct = product;
    }
    
    protected override void ClosePrompt()
    {
        prompt.SetActive(false);
    }
    
    public override void Buy()
    {
        // Play cash sound effect
        AudioManager.Instance.PlaySound(SoundType.Cash, 1);
        
        playerInfo.AddProduct(selectedProduct);
        currencyManager.UpdateCurrency(-selectedProduct.Cost);
        ClosePrompt();
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
}