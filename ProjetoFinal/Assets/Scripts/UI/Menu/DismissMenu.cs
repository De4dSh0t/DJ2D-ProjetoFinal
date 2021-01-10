using UnityEngine;

public class DismissMenu : Menu<Cleaner>
{
    [Header("Cleaner Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    private Cleaner selectedCleaner;
    
    protected override void DisplayList()
    {
        foreach (var kvp in cleanerManager.HiredCleaners)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            HiringButton hButton = button.GetComponent<HiringButton>();

            hButton.Setup(kvp.Value);
            hButton.OnSelect += ShowPrompt;
            spawnedButtons.Add(button);
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
    
    public void DismissCleaner()
    {
        cleanerManager.RemoveCleaner(cleanerManager.GetCleanerByID(selectedCleaner.CleanerID));
        ClearList();
        DisplayList();
        ClosePrompt();
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
}