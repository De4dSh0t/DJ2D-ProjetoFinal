using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private int startingCurrency;
    private int coinsPerSecond;
    
    [Header("Dependencies Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    [SerializeField] private GuestManager guestManager;
    
    [Header("UI Settings")]
    [SerializeField] private TMP_Text currencyText;
    [SerializeField] private TMP_Text earnText;
    
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
            coinsPerSecond = 0;
            
            // Wage (Cleaners)
            coinsPerSecond += -cleanerManager.GetTotalExpenses();
            
            // Reward (Guests)
            coinsPerSecond += guestManager.GuestCount;
            
            CurrentCurrency += coinsPerSecond;
            
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

        if (coinsPerSecond >= 0)
        {
            earnText.text = $"+ {coinsPerSecond}";
            earnText.color = Color.green;
        }
        else
        {
            earnText.text = coinsPerSecond.ToString();
            earnText.color = Color.red;
        }
    }
}