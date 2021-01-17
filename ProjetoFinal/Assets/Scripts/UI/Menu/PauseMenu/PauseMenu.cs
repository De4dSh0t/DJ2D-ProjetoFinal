using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnButton;
    
    [Header("Screen Settings")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    
    [Header("Save Settings")]
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private CleanerManager cleanerManager;
    
    private void Start()
    {
        // Add listeners
        continueButton.onClick.AddListener(Continue);
        optionsButton.onClick.AddListener(Options);
        returnButton.onClick.AddListener(Return);
    }
    
    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        // Activate/Deactivate Pause Menu
        if (!pauseScreen.activeInHierarchy && !optionsScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            GameManager.Instance.GameIsPaused = true;
        }
        else if (pauseScreen.activeInHierarchy && !optionsScreen.activeInHierarchy)
        {
            Continue();
        }
    }
    
    private void Continue()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        GameManager.Instance.GameIsPaused = false;
    }
    
    private void Options()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
    
    private void Return()
    {
        // Save current progress
        SaveData saveData = new SaveData()
        {
            currency = currencyManager.CurrentCurrency,
            currentScene = SceneManager.GetActiveScene().name,
            hiredCleaners = cleanerManager.GetAllCleaners()
        };
        SaveManager.Save(saveData);
        
        // Return to main menu
        SceneManager.LoadScene("MainMenu");
    }
}