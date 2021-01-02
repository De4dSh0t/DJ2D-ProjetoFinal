using UnityEngine;

[System.Serializable]
public struct ActionZone
{
    [SerializeField] private string actionID;
    [SerializeField] private Vector3Int waypoint;

    public string ActionID => actionID;
    public Vector3Int Waypoint => waypoint;
    public bool InUse { get; set; }
}