using UnityEngine;

public class DismissMenu : Menu<Cleaner>
{
    [Header("Cleaner Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    private Cleaner selectedCleaner;

    [Header("CharacterSelection Settings")] 
    [SerializeField] private CharacterSelector characterSelector;
    [SerializeField] private CameraDrag cameraDrag;
    [SerializeField] private CameraMovement cameraMovement;
    
    protected override void OnEnable()
    {
        EnableCharacterSelection();
        closeButton.onClick.AddListener(Close);
    }

    private void EnableCharacterSelection()
    {
        characterSelector.enabled = true;
        cameraDrag.enabled = true;
        cameraMovement.enabled = false;
    }
    
    private void DisableCharacterSelection()
    {
        characterSelector.enabled = false;
        cameraDrag.enabled = false;
        cameraMovement.enabled = true;
    }
    
    protected override void Close()
    {
        DisableCharacterSelection();
        gameObject.SetActive(false);
        computerScreen.SetActive(true);
    }
    
    protected override void ShowPrompt(Cleaner cleaner)
    {
        prompt.SetActive(true);
        selectedCleaner = cleaner;
    }
    
    protected override void ClosePrompt()
    {
        prompt.SetActive(false);
    }
    
    public override void Cancel()
    {
        print("Canceled.");
        ClosePrompt();
    }
}