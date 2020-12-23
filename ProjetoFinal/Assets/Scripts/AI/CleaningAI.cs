using UnityEngine;

public class CleaningAI : AISystem
{
    [Header("Garbage Settings")]
    [SerializeField] private GarbageGenerator garbageGenerator;
    [SerializeField] private int maxCarryingCapacity;
    private bool firstScan = true;
    private int garbageCount;

    /// <summary>
    /// Used to rescan the room
    /// </summary>
    public bool GarbageFound { get; set; }

    private void Start()
    {
        DecisionMaking();
    }

    public override void DecisionMaking()
    {
        // Goes to the garbage room if it the entity has reached its full carrying capacity
        if (garbageCount >= maxCarryingCapacity)
        {
            Room gRoom = SearchRoom("GarbageRoom");
            if (gRoom != null) SetState(new ChangeRoomState(this, gRoom));
            garbageCount = 0;
            return;
        }
        
        if (firstScan) // First Scan
        {
            SetState(new ScanRoomState(this, CurrentRoom, garbageGenerator));
            firstScan = false;
        }
        else
        {
            if (GarbageFound) // After garbage found, scan once again the room to check for more garbage
            {
                SetState(new ScanRoomState(this, CurrentRoom, garbageGenerator));
            }
            else // Change room if no more garbage was found
            {
                SetState(new ChangeRoomState(this, rooms[Random.Range(0, rooms.Length)]));
                firstScan = true;
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
}
