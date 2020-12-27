using System.Collections.Generic;
using UnityEngine;

public abstract class AISystem : StateMachine
{
    [Header("Zone Settings")]
    public Zone[] zones;

    [Header("Movement Settings")]
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private float speed;
    private readonly Vector3 offset = new Vector3(0.5f, 0.5f);
    private Stack<Node> path;
    
    /// <summary>
    /// Returns entity position in Vector3Int
    /// </summary>
    public Vector3Int PositionInt => Vector3Int.RoundToInt(transform.position - offset);
    
    /// <summary>
    /// Returns predefined speed
    /// </summary>
    public float Speed => speed;

    /// <summary>
    /// Returns pathfinding component
    /// </summary>
    public Pathfinding Pathfinding => pathfinding;

    /// <summary>
    /// Returns current zone scriptable object
    /// </summary>
    public Zone CurrentZone
    {
        get
        {
            foreach (var zone in zones)
            {
                if (zone.ZoneTilemap.HasTile(PositionInt)) return zone;
            }

            return null;
        }
    }

    /// <summary>
    /// Searches for a area by the specified ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Zone SearchZone(string id)
    {
        foreach (var zone in zones)
        {
            if (zone.ZoneID == id)
            {
                return zone;
            }
        }

        return null;
    }

    public virtual void DecisionMaking() {}
}
