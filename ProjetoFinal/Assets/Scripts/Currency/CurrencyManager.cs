using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private int startingCurrency;
    
    [Header("Dependencies Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    [SerializeField] private GuestManager guestManager;
    
    [Header("UI Settings")]
    [SerializeField] private TMP_Text currencyText;
    
    // Save Settings
    private SaveData saveData;
    
    public int CurrentCurrency { get; private set; }
    
    private void Start()
    {
        CurrentCurrency = startingCurrency;
        DisplayCurrency();
        
        StartCoroutine(UpdateCurrency());
    }
    
    public void UpdateCurrency(int value)
    {
        CurrentCurrency += value;
        
        if (CurrentCurrency <= 0) CurrentCurrency = 0;
        
        DisplayCurrency();
    }
    
    private IEnumerator UpdateCurrency()
    {
        while (true)
        {
            int value = 0;
            
            // Wage (Cleaners)
            value += -cleanerManager.GetTotalExpenses();
            
            // Reward (Guests)
            value += guestManager.GuestCount;
            
            CurrentCurrency += value;
            
            if (CurrentCurrency <= 0)
            {
                CurrentCurrency = 0;
                
                // Dismiss all cleaners
                cleanerManager.DismissAllCleaners();
                
                print("No more money!");
            }
            
            DisplayCurrency();
            yield return new WaitForSeconds(1);
        }
    }
    
    private void DisplayCurrency()
    {
        currencyText.text = CurrentCurrency.ToString();
    }
}