using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] private int startingCurrency;
    
    public int CurrentCurrency { get; private set; }
    
    private void Start()
    {
        CurrentCurrency = startingCurrency;
    }
    
    public void UpdateCurrency(int value)
    {
        CurrentCurrency += value;
        
        if (CurrentCurrency <= 0) CurrentCurrency = 0;
        
        print(CurrentCurrency);
    }
}