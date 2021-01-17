using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    
    [Header("Screen Settings")]
    [SerializeField] private GameObject optionsScreen;
    
    public void Awake()
    {
        CheckSaveFile();
        
        // Add Listeners
        continueButton.onClick.AddListener(ContinueGame);
        newGameButton.onClick.AddListener(NewGame);
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
    }
    
    private void NewGame()
    {
        SceneManager.LoadScene("Level1");
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