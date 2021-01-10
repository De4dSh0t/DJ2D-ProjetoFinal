using UnityEngine;
using UnityEngine.UI;

public class ProductStore : Store<CleaningProduct>
{
    protected override void DisplayList()
    {
        foreach (var product in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            ProductButton pButton = button.GetComponent<ProductButton>();
            
            pButton.Setup(product.ProductID, product.NumberOfUses, product.Cost);
            pButton.OnSelect.AddListener(ShowPrompt);
            spawnedButtons.Add(button);
            
            // Disables button interaction if player doesn't have enough money to buy
            if (product.Cost > currencyManager.CurrentCurrency) pButton.GetComponent<Button>().interactable = false;
        }
    }
    
    protected override void ShowPrompt()
    {
        prompt.SetActive(true);
    }
    
    protected override void ClosePrompt()
    {
        prompt.SetActive(false);
    }
    
    public override void Buy()
    {
        print("Bought.");
        ClosePrompt();
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
}