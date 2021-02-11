using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // Guest Settings
    private List<GuestStatus> guests;
    
    [Header("Level Settings")]
    [SerializeField] private float requiredPoints;
    [SerializeField] private CurrencyManager currencyManager;
    
    [Header("Score Screen Settings")]
    [SerializeField] private ScoreSetup scoreScreen;
    
    private void Start()
    {
        GameManager.Instance.OnLevelEnd += CalculateResult;
    }
    
    private void CalculateResult()
    {
        float coins = currencyManager.CurrentCurrency;
        
        if (coins >= requiredPoints)
        {
            scoreScreen.Setup(requiredPoints, coins, true);
            
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SaveManager.Save(new SaveData {currentScene = nextScene});
        }
        else
        {
            scoreScreen.Setup(requiredPoints, coins, false);
            SaveManager.DeleteSave();
        }
    }
}