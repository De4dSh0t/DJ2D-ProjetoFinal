using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.UI;

public class CleanerMenu : Menu<CleanerInfo>
{
    [Header("Cleaner Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    [SerializeField] private CleanerGenerator cleanerGenerator;
    private readonly List<HiringButton> hiringButtons = new List<HiringButton>(); 
    private CleanerInfo selectedCleanerInfo;

    protected override void DisplayList()
    {
        foreach (var info in cleanerGenerator.GenerateCleaners(5))
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            HiringButton hButton = button.GetComponent<HiringButton>();
            
            hButton.Setup(info);
            hButton.OnSelect += ShowPrompt;
            spawnedButtons.Add(button);
            hiringButtons.Add(hButton);
            
            // Disables button interaction if player doesn't have enough money to buy
            if (info.Wage > currencyManager.CurrentCurrency) hButton.GetComponent<Button>().interactable = false;
        }
    }

    protected override void ShowPrompt(CleanerInfo cleanerInfo)
    {
        prompt.SetActive(true);
        selectedCleanerInfo = cleanerInfo;
    }
    
    protected override void ClosePrompt()
    {
        prompt.SetActive(false);
    }
    
    public override void Buy()
    {
        // Play cash sound effect
        AudioManager.Instance.PlaySound(SoundType.Cash, 1);
        
        cleanerManager.AddCleaner(selectedCleanerInfo);
        RemoveButton(selectedCleanerInfo);
        ClosePrompt();
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
    
    private void RemoveButton(CleanerInfo info)
    {
        foreach (var hButton in hiringButtons)
        {
            if (hButton.cleanerInfo.CleanerID == info.CleanerID)
            {
                Destroy(hButton.gameObject);
                hiringButtons.Remove(hButton);
                break;
            }
        }
    }
}