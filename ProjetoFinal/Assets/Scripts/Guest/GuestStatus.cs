using System.Collections.Generic;
using UnityEngine;

public class GuestStatus : MonoBehaviour
{
    [Header("Evaluation Settings")]
    [SerializeField] private float startingPoints;
    [SerializeField] private float pointsPerGarbage;
    private HashSet<Garbage> encounteredGarbage;
    
    private void Start()
    {
        encounteredGarbage = new HashSet<Garbage>();
    }
    
    public void AddGarbage(GameObject garbage)
    {
        encounteredGarbage.Add(garbage.GetComponent<Garbage>());
    }

    public float GetEvaluation()
    {
        float evaluation = startingPoints;
        
        foreach (var garbage in encounteredGarbage)
        {
            // Check if the garbage was spawned by this entity (to ignore that garbage)
            if (garbage.SpawnedBy == GetInstanceID()) continue;
            
            evaluation -= pointsPerGarbage;
        }
        
        return evaluation;
    }
}