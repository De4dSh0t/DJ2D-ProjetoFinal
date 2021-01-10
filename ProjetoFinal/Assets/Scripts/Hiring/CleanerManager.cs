using System.Collections.Generic;
using UnityEngine;

public class CleanerManager : MonoBehaviour
{
    [Header("Instantiate Settings")]
    [SerializeField] private GameObject cleanerPrefab;
    [SerializeField] private Vector3 spawnPos;
    
    public Dictionary<GameObject, Cleaner> HiredCleaners { get; } = new Dictionary<GameObject, Cleaner>();
    
    public void AddCleaner(Cleaner cleaner)
    {
        GameObject newCleaner = Instantiate(cleanerPrefab, spawnPos, Quaternion.identity);
        newCleaner.GetComponent<CleaningAI>().Setup(cleaner.CarryingCapacity, cleaner.MovementSpeed);
        
        HiredCleaners.Add(newCleaner, cleaner);
    }
    
    public void RemoveCleaner(GameObject cleaner)
    {
        HiredCleaners.Remove(cleaner);
        Destroy(cleaner);
    }
    
    public GameObject GetCleanerByID(string id)
    {
        foreach (var kvp in HiredCleaners)
        {
            if (kvp.Value.CleanerID == id) return kvp.Key;
        }
        
        print($"No cleaner found named {id}!");
        
        return null;
    }
    
    public int GetTotalExpenses()
    {
        int total = 0;
        
        foreach (var cleaner in HiredCleaners.Values)
        {
            total += cleaner.Wage;
        }
        
        return total;
    }
}