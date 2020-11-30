using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset;
    private Camera currentCamera;

    void Start()
    {
        currentCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        //Smoothly follow the current target
        currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, target.position + offset, smooth);
    }
    
    public void UpdateTarget(Transform nextTarget)
    { 
        target = nextTarget;
    }
}
