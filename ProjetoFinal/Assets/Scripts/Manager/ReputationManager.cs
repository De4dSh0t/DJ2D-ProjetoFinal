using System;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    [Header("Guest Settings")]
    [SerializeField] private GuestManager guestManager;
    private List<GuestStatus> guests;
    
    [Header("Level Settings")]
    [SerializeField] private float requiredPoints;
    
    [Header("Score Screen Settings")]
    [SerializeField] private ScoreSetup scoreScreen;
    
    private void Start()
    {
        GameManager.Instance.OnLevelEnd += CalculateReputation;
    }
    
    private void CalculateReputation()
    {
        guests = guestManager.GetAllGuests();
        float points = 0;
        
        foreach (var guest in guests)
        {
            points += guest.GetEvaluation();
            print(guest.GetInstanceID() + ": " + guest.GetEvaluation());
        }
        
        scoreScreen.Setup(requiredPoints, points);
    }
}