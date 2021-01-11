using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Header("Camera Settings")] 
    [SerializeField] private Camera main;
    [SerializeField] private CameraDrag camera;

    [Header("Character Settings")]
    [SerializeField] private LayerMask characterLayer;
    private RaycastHit2D hitInfo;
    
    [Header("Pop-Up Settings")]
    [SerializeField] private GameObject popUp;
    [SerializeField] private CleanerManager cleanerManager;
    private GameObject currentSelection;
    
    void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Checks whether the mouse is touching a character or not (if not, returns null)
            hitInfo = Physics2D.Raycast(main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10, characterLayer);

            // Updates the current camera target
            camera.UpdateTarget(hitInfo.transform);
            
            // Dismiss Pop-Up
            if (hitInfo.transform == null)
            {
                popUp.SetActive(false);
                return;
            }
            
            currentSelection = hitInfo.transform.gameObject;
            popUp.SetActive(true);
            popUp.GetComponent<DismissPopUp>().Setup(currentSelection, cleanerManager.GetCleanerByObject(currentSelection));
        }
    }
}