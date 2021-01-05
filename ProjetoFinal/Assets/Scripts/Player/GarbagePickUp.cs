using UnityEngine;

public class GarbagePickUp : MonoBehaviour
{
    [Header("Collider Settings")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask contactLayer;

    [Header("Player Info Settings")]
    [SerializeField] private PlayerInfo playerInfo;
    private Garbage garbageToPick;
    
    // Pick-Up Settings
    private float holdingTime;

    void Start()
    {
        
    }
    
    void Update()
    {
        CheckCollision();
        HandlePickUp();
    }

    private void HandlePickUp()
    {
        if (garbageToPick == null) return;
        
        // Pick-Up
        if (Input.GetKey(KeyCode.E))
        {
            holdingTime += Time.deltaTime;
            
            if (holdingTime < garbageToPick.CleaningTime) return;
            
            Destroy(garbageToPick.gameObject);
            playerInfo.CarryingCount++;
        }
        
        // Reset holding time
        if (Input.GetKeyUp(KeyCode.E)) holdingTime = 0;
    }

    private void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0, contactLayer);
        
        //Checks if it's touching a garbage object
        if (hit)
        {
            garbageToPick = hit.collider.GetComponent<Garbage>();
            return;
        }

        garbageToPick = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
