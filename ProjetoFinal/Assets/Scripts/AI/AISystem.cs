using System.Collections.Generic;
using UnityEngine;

public class AISystem : StateMachine
{
    [Header("Movement Settings")] 
    [SerializeField] private Transform waypoint;
    [SerializeField] private Pathfinding pathfinding;
    public float speed;
    private Stack<Node> path;
    private Vector3 offset = new Vector3(0.5f, 0.5f);
    
    void Start()
    {
        Debug.Log(Vector3Int.RoundToInt(transform.position - offset));
        SetState(new FindState(this, pathfinding, 
            Vector3Int.RoundToInt(transform.position - offset), Vector3Int.RoundToInt(waypoint.position - offset)));
    }
}
