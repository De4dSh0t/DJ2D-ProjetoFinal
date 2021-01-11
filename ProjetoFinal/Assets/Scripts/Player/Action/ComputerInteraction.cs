using UnityEngine;

public class ComputerInteraction : PlayerAction
{
    [Header("Collider Settings")]
    [SerializeField] private LayerMask computerLayer;
    private ContactFilter2D filter;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject computerMenu;
    
    private void Start()
    {
        pCollider = GetComponent<Collider2D>();
        
        // Setup filter to ignore collisions (except with the "computerLayer")
        filter = new ContactFilter2D
        {
            layerMask = computerLayer,
            useLayerMask = true,
            useTriggers = true
        };
    }
    
    private void Update()
    {
        
    }
    
    private void HandleInteraction()
    {
        // Tries to get the closest object
        GameObject closestObject = GetClosestObject(filter);
        if (closestObject == null) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}