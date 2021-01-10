using System.Collections.Generic;
using UnityEngine;

public class CleanerManager : MonoBehaviour
{
    [Header("Instantiate Settings")]
    [SerializeField] private GameObject cleanerPrefab;
    [SerializeField] private Vector3 spawnPos;
    
    public List<GameObject> HiredCleaners { get; } = new List<GameObject>();
    
    public void AddCleaner(Cleaner cleaner)
    {
        GameObject newCleaner = Instantiate(cleanerPrefab, spawnPos, Quaternion.identity);
        newCleaner.GetComponent<CleaningAI>().Setup(cleaner.CarryingCapacity, cleaner.MovementSpeed);
        
        HiredCleaners.Add(newCleaner);
    }
    
    public void RemoveCleaner(GameObject cleaner)
    {
        HiredCleaners.Remove(cleaner);
    }
    
    public GameObject GetCleanerByID(string id)
    {
        foreach (var cleaner in HiredCleaners)
        {
            if (cleaner.GetComponent<Cleaner>().CleanerID == id) return cleaner;
        }
        
        print($"No cleaner found named {id}!");
        
        return null;
    }
    
    public int GetTotalExpenses()
    {
        int total = 0;
        
        foreach (var cleaner in HiredCleaners)
        {
            total += cleaner.GetComponent<Cleaner>().Wage;
        }
        
        return total;
    }
}