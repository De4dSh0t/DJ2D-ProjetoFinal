﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSetup : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text requiredPoints;
    [SerializeField] private TMP_Text playerPoints;
    [SerializeField] private TMP_Text stateText;
    private bool canUpdate;
    private bool state;
    private bool first = true;
    
    // Timer Settings
    private float startingTime;
    
    private void Update()
    {
        if (!canUpdate) return;
        if (Time.time - startingTime <= 1.5f) return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (state)
            {
                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    
    public void Setup(float requiredP, float playerP, bool s)
    {
        if (!first) return;
        
        requiredPoints.text = requiredP.ToString();
        playerPoints.text = playerP.ToString();
        state = s;
        
        if (state)
        {
            stateText.text = "You did it!";
            stateText.color = Color.green;
        }
        else
        {
            stateText.text = "Try again...";
            stateText.color = Color.red;
        }
        
        canUpdate = true;
        first = true;
        startingTime = Time.time;
    }
}