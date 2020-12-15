using System.Collections.Generic;
using UnityEngine;

public class AISystem : StateMachine
{
    [Header("Movement Settings")] 
    [SerializeField] private Transform waypoint;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private float speed;
    private readonly Vector3 offset = new Vector3(0.5f, 0.5f);
    private Stack<Node> path;

    /// <summary>
    /// Returns entity position in Vector3Int
    /// </summary>
    public Vector3Int PositionInt => Vector3Int.RoundToInt(transform.position - offset);

    /// <summary>
    /// Returns waypoint position in Vector3Int
    /// </summary>
    public Vector3Int WaypointInt => Vector3Int.RoundToInt(waypoint.position - offset);

    /// <summary>
    /// Returns predefined speed
    /// </summary>
    public float Speed => speed;

    /// <summary>
    /// Returns pathfinding component
    /// </summary>
    public Pathfinding Pathfinding => pathfinding;
    
    void Start()
    {
        SetState(new FindState(this, pathfinding, PositionInt, WaypointInt));
    }
}
