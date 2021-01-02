using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Zone", menuName = "ScriptableObjects/Zone/Zone", order = 1)]
public class Zone : ScriptableObject
{
    [Header("General Settings")] [SerializeField]
    private string zoneID;

    [SerializeField] private Vector3Int entryWaypoint;
    [SerializeField] private Tilemap zoneTilemap;

    [Header("Action Settings")]
    [SerializeField] private ActionZone[] actionZones;

    public string ZoneID => zoneID;
    public Vector3Int EntryWaypoint => entryWaypoint;
    public Tilemap ZoneTilemap => zoneTilemap;
    public ActionZone[] ActionZones => actionZones;
    
    /// <summary>
    /// Returns an available ActionZone struct
    /// </summary>
    public ActionZone AvailableActionZone
    {
        get
        {
            foreach (var zone in actionZones)
            {
                if (!zone.InUse) return zone;
            }

            return new ActionZone();
        }
    }
}