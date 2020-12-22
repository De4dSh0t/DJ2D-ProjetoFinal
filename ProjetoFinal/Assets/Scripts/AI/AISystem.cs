using System.Collections.Generic;
using UnityEngine;

public abstract class AISystem : StateMachine
{
    [Header("Room Settings")]
    public Room[] rooms;

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
    /// Returns current room scriptable object
    /// </summary>
    public Room CurrentRoom
    {
        get
        {
            foreach (var r in rooms)
            {
                if (r.room.HasTile(PositionInt)) return r;
            }

            return null;
        }
    }

    public virtual void DecisionMaking() {}
}
