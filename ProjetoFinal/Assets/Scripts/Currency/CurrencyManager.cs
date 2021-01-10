using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private int startingCurrency;
    
    [Header("UI Settings")]
    [SerializeField] private TMP_Text currencyText;
    
    public int CurrentCurrency { get; private set; }
    
    private void Start()
    {
        CurrentCurrency = startingCurrency;
        DisplayCurrency();
    }
    
    public void UpdateCurrency(int value)
    {
        CurrentCurrency += value;
        
        if (CurrentCurrency <= 0) CurrentCurrency = 0;
        
        DisplayCurrency();
    }
    
    private void DisplayCurrency()
    {
        currencyText.text = $"Coins: {CurrentCurrency}";
    }
}