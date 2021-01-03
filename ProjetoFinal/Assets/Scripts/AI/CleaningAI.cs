using UnityEngine;

public class CleaningAI : AISystem
{
    [Header("Garbage Settings")]
    [SerializeField] private GarbageGenerator garbageGenerator;
    [SerializeField] private int maxCarryingCapacity;
    private bool firstScan = true;
    private int garbageCount;
    
    // Emotional System Settings
    private EmotionalSystem emotionalSystem;
    
    // Decision Settings
    private int sIndex;

    /// <summary>
    /// Used to rescan the room
    /// </summary>
    public bool GarbageFound { get; set; }

    /// <summary>
    /// Returns EmotionalSystem reference
    /// </summary>
    public EmotionalSystem EmotionalSystem => emotionalSystem;

    private void Start()
    {
        emotionalSystem = GetComponent<EmotionalSystem>();
        
        DecisionMaking();
    }

    public override void DecisionMaking()
    {
        HandleStates();

        switch (sIndex)
        {
            case 0: // Scan Room
            {
                SetState(new ScanRoomState(this, CurrentZone, garbageGenerator));
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
        }
    }

    public void PickUp(Garbage garbage)
    {
        // Garbage Count
        if (garbageCount >= maxCarryingCapacity) return;
        garbageCount++;
        
        //Destroy & Remove from list
        garbageGenerator.spawnedGarbage.Remove(garbage);
        Destroy(garbage.gameObject);
    }

    private void HandleStates()
    {
        // Tired
        if (emotionalSystem.GetEnergy() <= 10)
        {
            sIndex = 3;
            return;
        }
        
        // Goes to the garbage room if the entity has reached its full carrying capacity
        if (garbageCount >= maxCarryingCapacity)
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
}
