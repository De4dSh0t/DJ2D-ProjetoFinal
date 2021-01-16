using UnityEngine;

public class CleaningAI : AISystem
{
    // Info Settings
    private CleanerInfo cleanerInfo;
    private GarbageManager garbageManager;
    private bool firstScan = true;
    private int garbageCount;
    
    // Decision Settings
    private int sIndex;

    /// <summary>
    /// Used to rescan the room
    /// </summary>
    public bool GarbageFound { get; set; }
    
    /// <summary>
    /// Used to change state to Dismiss State
    /// </summary>
    public bool HasBeenDismissed { get; set; }
    
    private void Start()
    {
        garbageManager = FindObjectOfType<GarbageManager>();
        
        // Setup movement speed
        speed = cleanerInfo.MovementSpeed;
        
        DecisionMaking();
    }
    
    public void Setup(CleanerInfo info)
    {
        cleanerInfo = info;
    }
    
    public override void DecisionMaking()
    {
        HandleStates();

        switch (sIndex)
        {
            case 0: // Scan Room
            {
                SetState(new ScanRoomState(this, CurrentZone, garbageManager));
                break;
            }
            case 1: // Change Room (Randomly)
            {
                SetState(new ChangeRoomState(this, zones[Random.Range(0, zones.Length)]));
                break;
            }
            case 2: // Go to Garbage Room
            {
                Zone gRoom = SearchZone("GarbageRoom");
                if (gRoom != null) SetState(new ChangeRoomState(this, gRoom));
                break;
            }
            case 3: // Go to Breakroom
            {
                Zone bRoom = SearchZone("BreakRoom");
                
                // Is already in the breakroom
                if (CurrentZone == bRoom)
                {
                    SetState(new IdleState(this));
                    return;
                }
                
                if (bRoom != null) SetState(new ChangeRoomState(this, bRoom));
                break;
            }
            case 4:
            {
                SetState(new DismissState(this, SearchZone("InnEntry")));
                break;
            }
        }
    }

    public void PickUp(Garbage garbage)
    {
        //Destroy & Remove from list
        if (!garbageManager.SpawnedGarbage.Contains(garbage)) return;
        garbageManager.RemoveGarbage(garbage);
        Destroy(garbage.gameObject);
        
        // Garbage Count
        if (garbageCount >= cleanerInfo.CarryingCapacity) return;
        garbageCount++;
    }

    private void HandleStates()
    {
        // Dismiss
        if (HasBeenDismissed)
        {
            sIndex = 4;
            return;
        }
        
        // Goes to the garbage room if the entity has reached its full carrying capacity
        if (garbageCount >= cleanerInfo.CarryingCapacity)
        {
            garbageCount = 0;
            sIndex = 2;
            return;
        }
        
        if (firstScan) // First Scan
        {
            sIndex = 0;
            firstScan = false;
        }
        else
        {
            if (GarbageFound) // After garbage found, scan once again the room to check for more garbage
            {
                sIndex = 0;
            }
            else // Change room if no more garbage was found
            {
                sIndex = 1;
                firstScan = true;
            }
        }
    }
    
    public void DestroyCleaner()
    {
        Destroy(gameObject);
    }
}
