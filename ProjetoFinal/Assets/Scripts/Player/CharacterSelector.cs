using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [Header("Camera Settings")] 
    [SerializeField] private Camera main;
    [SerializeField] private CameraBehaviour camera;

    [Header("Character Settings")]
    [SerializeField] private LayerMask characterLayer;
    private RaycastHit2D hitInfo;

    void Update()
    {
        HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Checks whether the mouse is touching a character or not (if not, returns null)
            hitInfo = Physics2D.Raycast(main.ScreenToWorldPoint(Input.mousePosition), Vector2.up, 12, characterLayer);

            // Updates the current camera target
            camera.UpdateTarget(hitInfo.transform);
        }
    }
}
