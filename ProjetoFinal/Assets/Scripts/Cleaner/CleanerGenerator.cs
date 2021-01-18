using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CleanerGenerator : MonoBehaviour
{
    [Header("Generator Settings")]
    [SerializeField] private List<string> names;
    [Tooltip("X:Min | Y:Max")] [SerializeField] private Vector2Int carryingCapacity;
    [Tooltip("X:Min | Y:Max")] [SerializeField] private Vector2 movementSpeed;
    
    public List<CleanerInfo> GenerateCleaners(int nCleaners)
    {
        List<CleanerInfo> cleaners = new List<CleanerInfo>();
        List<string> possibleNames = names;
        
        for (int i = 0; i < nCleaners; i++)
        {
            string id = possibleNames[Random.Range(0, possibleNames.Count)];
            int capacity = Random.Range(carryingCapacity.x, carryingCapacity.y);
            float speed = (float) Math.Round(Random.Range(movementSpeed.x, movementSpeed.y), 2);
            int wage = CalculateWage(capacity, speed);
            
            CleanerInfo info = new CleanerInfo(id, capacity, speed, wage, 0);
            cleaners.Add(info);
            
            // Removes name to prevent duplicates
            possibleNames.Remove(id);
        }
        
        return cleaners;
    }
    
    private int CalculateWage(int capacity, float speed)
    {
        float temp = capacity + speed;
        
        if (temp >= 8 && temp < 10) return 4;
        if (temp >= 6 && temp < 8) return 3;
        if (temp >= 4 && temp < 6) return 2;
        return 1;
    }
}