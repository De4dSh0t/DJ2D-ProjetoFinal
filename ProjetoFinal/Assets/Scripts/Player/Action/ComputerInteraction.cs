using UnityEngine;

public class ComputerInteraction : PlayerAction
{
    [Header("Collider Settings")]
    [SerializeField] private LayerMask computerLayer;
    private ContactFilter2D filter;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject computerMenu;
    
    [Header("Lock Settings")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAction playerAction;
    
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
        HandleInteraction();
    }
    
    private void HandleInteraction()
    {
        // Tries to get the closest object
        GameObject closestObject = GetClosestObject(filter);
        if (closestObject == null) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (computerMenu.activeInHierarchy) return;
            
            // Activates computer menu
            computerMenu.SetActive(true);
            
            // Deactivates player movement and player cleaning action
            playerMovement.enabled = false;
            playerAction.enabled = false;
        }
    }

    public void Unlock()
    {
        // Used on the close button of the computer menu
        playerMovement.enabled = true;
        playerAction.enabled = true;
    }
}