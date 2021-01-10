using UnityEngine;
using UnityEngine.UI;

public class CleanerMenu : Menu<Cleaner>
{
    [Header("Cleaner Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    private Cleaner selectedCleaner;

    protected override void DisplayList()
    {
        foreach (var cleaner in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            HiringButton hButton = button.GetComponent<HiringButton>();
            
            hButton.Setup(cleaner);
            hButton.OnSelect += ShowPrompt;
            spawnedButtons.Add(button);
            
            // Disables button interaction if player doesn't have enough money to buy
            print(currencyManager.CurrentCurrency);
            if (cleaner.Wage > currencyManager.CurrentCurrency) hButton.GetComponent<Button>().interactable = false;
        }
    }
    
    protected override void ShowPrompt(Cleaner cleaner)
    {
        prompt.SetActive(true);
        selectedCleaner = cleaner;
    }
    
    protected override void ClosePrompt()
    {
        prompt.SetActive(false);
    }
    
    public override void Buy()
    {
        print("Bought.");
        cleanerManager.AddCleaner(selectedCleaner);
        ClosePrompt();
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
}