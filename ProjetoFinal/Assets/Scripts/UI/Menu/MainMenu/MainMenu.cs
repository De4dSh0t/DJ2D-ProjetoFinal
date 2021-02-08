using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button guideButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    
    [Header("Screen Settings")] 
    [SerializeField] private GameObject guideScreen;
    [SerializeField] private GameObject optionsScreen;
    
    public void Awake()
    {
        CheckSaveFile();
        
        // Add Listeners
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(NewGame);
        guideButton.onClick.AddListener(Guide);
        optionsButton.onClick.AddListener(Options);
        quitButton.onClick.AddListener(QuitGame);
    }
    
    private void CheckSaveFile()
    {
        // Deactivates continue button if save file hasn't been found
        if (!SaveManager.ContainsSave()) continueButton.interactable = false;
    }
    
    private void ContinueGame()
    {
        print("Continue!");
        SceneManager.LoadScene("Level1");
    }
    
    private void NewGame()
    {
        SaveManager.DeleteSave();
        SceneManager.LoadScene("Level1");
    }
    
    private void Guide()
    {
        gameObject.SetActive(false);
        guideScreen.SetActive(true);
    }
    
    private void Options()
    {
        gameObject.SetActive(false);
        optionsScreen.SetActive(true);
    }
    
    private void QuitGame()
    {
        Application.Quit();
    }
}