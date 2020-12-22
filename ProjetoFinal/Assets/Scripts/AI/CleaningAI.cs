using UnityEngine;

public class CleaningAI : AISystem
{
    [Header("Garbage Settings")]
    [SerializeField] private GarbageGenerator garbageGenerator;
    private bool firstScan = true;

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
        garbageGenerator.spawnedGarbage.Remove(garbage);
        Destroy(garbage.gameObject);
    }
}
