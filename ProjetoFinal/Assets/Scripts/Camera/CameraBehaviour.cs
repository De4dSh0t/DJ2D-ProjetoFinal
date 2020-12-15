using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    private bool canFocus;
    private Vector3 dragOrigin;

    [Header("Zoom Settings")]
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize; 
    [SerializeField] private float zoomRate;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset;
    private Camera currentCamera;

    void Start()
    {
        currentCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Disables camera drag and zoom when focused on a character
        if (canFocus) return; 
        
        DragCamera();
        HandleZoom();
    }

    void LateUpdate()
    {
        // Disables camera movement when not in focus on a character
        if (!canFocus) return;
        
        Focus();
    }

    private void Focus()
    {
        //Smoothly follow the current target
        currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, target.position + offset, smooth);
        
        //Smoothly zooms to the current target
        if (currentCamera.orthographicSize > minSize)
        {
            currentCamera.orthographicSize = Mathf.Lerp(currentCamera.orthographicSize, minSize, .1f);
        }
    }

    private void DragCamera()
    {
        // Set origin position when mouse button is first pressed
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            // Distance between the origin and the current mouse position within the camera
            Vector3 dist = dragOrigin - currentCamera.ScreenToWorldPoint(Input.mousePosition);

            // Move the camera by adding that distance
            currentCamera.transform.position += dist;
        }
    }

    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0) ZoomIn();
        if (Input.mouseScrollDelta.y < 0) ZoomOut();
    }

    private void ZoomIn()
    {
        currentCamera.orthographicSize = Mathf.Clamp(currentCamera.orthographicSize - zoomRate, minSize, maxSize);
    }

    private void ZoomOut()
    {
        currentCamera.orthographicSize = Mathf.Clamp(currentCamera.orthographicSize + zoomRate, minSize, maxSize);
    }
    
    public void UpdateTarget(Transform nextTarget)
    {
        // No Target
        if (nextTarget == null)
        {
            canFocus = false;
            return;
        }

        canFocus = true;
        target = nextTarget;
    }
}
