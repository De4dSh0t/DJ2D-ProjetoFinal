using System.Collections.Generic;
using UnityEngine;

public class GuestStatus : MonoBehaviour
{
    [Header("Evaluation Settings")]
    [SerializeField] private float startingPoints;
    [SerializeField] private int pointsPerGarbage;
    [SerializeField] private int maxGarbageDetected; // Used to control when the guest lefts the inn
    private HashSet<Garbage> encounteredGarbage;
    private CurrencyManager currencyManager;
    private GuestManager guestManager;
    private GuestAI guestAI;
    private int garbageCount;
    
    private void Start()
    {
        encounteredGarbage = new HashSet<Garbage>();
        currencyManager = FindObjectOfType<CurrencyManager>();
        guestManager = FindObjectOfType<GuestManager>();
        guestAI = GetComponent<GuestAI>();
    }
    
    public void AddGarbage(GameObject garbage)
    {
        Garbage detectedGarbage = garbage.GetComponent<Garbage>();
        
        // Check if garbage was spawned by guest
        if (detectedGarbage.SpawnedBy == guestAI.GetInstanceID()) return;
        
        // Try to save into hashset
        encounteredGarbage.Add(detectedGarbage);
        
        // Remove coins for the detected garbage (if hasn't been detected already)
        if (encounteredGarbage.Count > garbageCount)
        {
            currencyManager.UpdateCurrency(-pointsPerGarbage);
            garbageCount++;
        }
        
        // Check if guest has reached it's maximum numb of detected garbage (to quit the inn)
        if (garbageCount >= maxGarbageDetected)
        {
            guestAI.Exited = true;
            guestManager.RemoveGuest(gameObject);
        }
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