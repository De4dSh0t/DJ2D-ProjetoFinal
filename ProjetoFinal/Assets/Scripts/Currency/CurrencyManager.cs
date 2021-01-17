using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private int startingCurrency;
    
    [Header("Dependencies Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    
    [Header("UI Settings")]
    [SerializeField] private TMP_Text currencyText;
    
    // Save Settings
    private SaveData saveData;
    
    public int CurrentCurrency { get; private set; }
    
    private void Start()
    {
        // saveData = SaveManager.Load();
        //
        // if (saveData != null) CurrentCurrency = saveData.currency;
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
        bool canUpdate = true;
        
        while (canUpdate)
        {
            int value = 0;
            value += -cleanerManager.GetTotalExpenses();
            CurrentCurrency += value;

            if (CurrentCurrency <= 0) canUpdate = false;
            
            DisplayCurrency();
            yield return new WaitForSeconds(1);
        }
        
        print("No more money!");
    }
    
    private void DisplayCurrency()
    {
        currencyText.text = CurrentCurrency.ToString();
    }
}