using UnityEngine;

public class DismissMenu : Menu<CleanerInfo>
{
    [Header("Cleaner Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    private CleanerInfo selectedCleanerInfo;

    [Header("CharacterSelection Settings")] 
    [SerializeField] private CharacterSelector characterSelector;
    [SerializeField] private CameraDrag cameraDrag;
    [SerializeField] private CameraMovement cameraMovement;
    
    // Camera Settings
    private float cameraSize;

    private void Start()
    {
        // Get orthographicSize
        cameraSize = Camera.main.orthographicSize;
    }
    
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
        
        // Reset camera "zoom"
        Camera.main.orthographicSize = cameraSize;
    }
    
    protected override void Close()
    {
        DisableCharacterSelection();
        gameObject.SetActive(false);
        computerScreen.SetActive(true);
    }
    
    protected override void ShowPrompt(CleanerInfo cleanerInfo)
    {
        prompt.SetActive(true);
        selectedCleanerInfo = cleanerInfo;
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