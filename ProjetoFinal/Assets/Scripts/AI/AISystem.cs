using UnityEngine;

public class AISystem : StateMachine
{
    [Header("Movement Settings")] 
    [SerializeField] private Transform waypoint;
    [SerializeField] private Pathfinding pathfinding;
    public float speed;
    
    void Start()
    {
        SetState(new AIMove(this, pathfinding.FindPath(Vector3Int.RoundToInt(transform.position), Vector3Int.RoundToInt(waypoint.position))));
    }
    
    void Update()
    {
        
    }
}
