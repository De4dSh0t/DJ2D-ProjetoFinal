using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room", order = 1)]
public class Room : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] private string roomID;
    [SerializeField] private Vector3Int entryWaypoint;
    [SerializeField] private Tilemap roomTilemap;

    public string RoomID => roomID;
    public Vector3Int EntryWaypoint => entryWaypoint;
    public Tilemap RoomTilemap => roomTilemap;
    
    // Used by the CookingAI to go to this waypoint
    public Vector3Int deliverWaypoint;
}
