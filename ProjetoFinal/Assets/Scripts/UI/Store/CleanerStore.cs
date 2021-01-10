using UnityEngine;
using UnityEngine.UI;

public class CleanerStore : Store<Cleaner>
{
    protected override void DisplayList()
    {
        foreach (var cleaner in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            HiringButton hButton = button.GetComponent<HiringButton>();
            
            hButton.Setup(cleaner.CleanerID, cleaner.CarryingCapacity, cleaner.MovementSpeed, cleaner.Wage);
            hButton.OnSelect.AddListener(ShowPrompt);
            spawnedButtons.Add(button);
            
            // Disables button interaction if player doesn't have enough money to buy
            print(currencyManager.CurrentCurrency);
            if (cleaner.Wage > currencyManager.CurrentCurrency) hButton.GetComponent<Button>().interactable = false;
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