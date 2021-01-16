using System.Collections.Generic;
using UnityEngine;

public abstract class AISystem : StateMachine
{
    [Header("Zone Settings")]
    public Zone[] zones;
    
    [Header("Movement Settings")]
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] protected float speed;
    private readonly Vector3 offset = new Vector3(0.5f, 0.5f);
    private Stack<Node> path;
    
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    
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
    /// Searches for a zone by the specified ID
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

    /// <summary>
    /// Searches for an ActionZone by ID in the CurrentZone
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ActionZone SearchActionZone(string id)
    {
        foreach (var zone in CurrentZone.ActionZones)
        {
            if (zone.ActionID == id) return zone;
        }

        return new ActionZone();
    }
    
    /// <summary>
    /// Returns entity animator
    /// </summary>
    public Animator Animator => animator;

    public virtual void DecisionMaking() {}
}
