using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        if (points >= requiredPoints)
        {
            scoreScreen.Setup(requiredPoints, points, true);

            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SaveManager.Save(new SaveData {currentScene = nextScene});
        }
        else
        {
            scoreScreen.Setup(requiredPoints, points, false);
            SaveManager.DeleteSave();
        }
    }
}