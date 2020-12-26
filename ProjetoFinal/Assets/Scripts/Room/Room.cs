using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room", order = 1)]
public class Room : ScriptableObject
{
    public string roomID;
    public Vector3Int entryWaypoint;
    public Tilemap room;

    // Used by the CookingAI to go to this waypoint
    public Vector3Int deliverWaypoint;
}
