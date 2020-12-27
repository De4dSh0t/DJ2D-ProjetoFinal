using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Zone", menuName = "ScriptableObjects/Zone", order = 1)]
public class Zone : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] private string zoneID;
    [SerializeField] private Vector3Int entryWaypoint;
    [SerializeField] private Tilemap zoneTilemap;
    
    [Header("Action Settings")]
    [SerializeField] private Vector3Int actionWaypoint;

    public string ZoneID => zoneID;
    public Vector3Int EntryWaypoint => entryWaypoint;
    public Tilemap ZoneTilemap => zoneTilemap;
    public Vector3Int ActionWaypoint => actionWaypoint;
}
