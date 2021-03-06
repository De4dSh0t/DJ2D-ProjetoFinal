﻿using System.Collections.Generic;
using UnityEngine;

public class CleanerManager : MonoBehaviour
{
    [Header("Instantiate Settings")]
    [SerializeField] private GameObject cleanerPrefab;
    [SerializeField] private Vector3 spawnPos;
    
    private readonly Dictionary<GameObject, CleanerInfo> hiredCleaners = new Dictionary<GameObject, CleanerInfo>();

    public void AddCleaner(CleanerInfo cleanerInfo)
    {
        GameObject newCleaner = Instantiate(cleanerPrefab, spawnPos, Quaternion.identity);
        newCleaner.GetComponent<CleaningAI>().Setup(cleanerInfo);
        
        hiredCleaners.Add(newCleaner, cleanerInfo);
    }
    
    public void DismissCleaner(GameObject cleaner)
    {
        cleaner.GetComponent<CleaningAI>().HasBeenDismissed = true;
        cleaner.layer = 0;
        hiredCleaners.Remove(cleaner);
    }
    
    public void DismissAllCleaners()
    {
        if (hiredCleaners.Count == 0) return;
        
        foreach (var cleaner in hiredCleaners.Keys)
        {
            cleaner.GetComponent<CleaningAI>().HasBeenDismissed = true;
            cleaner.layer = 0;
        }
        
        hiredCleaners.Clear();
    }
    
    public CleanerInfo GetCleanerByObject(GameObject gameObj)
    {
        foreach (var kvp in hiredCleaners)
        {
            if (kvp.Key == gameObj)
            {
                CleanerInfo temp = kvp.Key.GetComponent<CleaningAI>().CleanerInfo;
                
                // Update value
                hiredCleaners[kvp.Key] = temp;
                
                return temp;
            }
        }
        
        print($"No cleaner found with {gameObj}!");
        
        return new CleanerInfo();
    }
    
    public int GetTotalExpenses()
    {
        int total = 0;
        
        foreach (var cleaner in hiredCleaners.Values)
        {
            total += cleaner.Wage;
        }
        
        return total;
    }
}