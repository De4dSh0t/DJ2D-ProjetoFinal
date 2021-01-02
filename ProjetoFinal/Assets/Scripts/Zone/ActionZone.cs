using UnityEngine;

[System.Serializable]
public struct ActionZone
{
    [SerializeField] private string actionID;
    [SerializeField] private Vector3Int waypoint;
    [SerializeField] private bool inUse;

    public string ActionID => actionID;
    public Vector3Int Waypoint => waypoint;
    public bool InUse 
    { 
        get => inUse;
        set => inUse = value;
    }
}