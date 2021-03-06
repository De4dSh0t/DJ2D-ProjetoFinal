﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button guideButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button returnButton;
    
    [Header("Screen Settings")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject guideScreen;
    [SerializeField] private GameObject optionsScreen;
    
    // Timer Settings
    private float startingTime;
    
    private void Start()
    {
        // Add listeners
        continueButton.onClick.AddListener(Continue);
        guideButton.onClick.AddListener(Guide);
        optionsButton.onClick.AddListener(Options);
        returnButton.onClick.AddListener(Return);
        
        startingTime = Time.time;
    }
    
    private void Update()
    {
        // Wait until starting animation ends
        if (Time.time - startingTime <= 3) return;
        
        HandleInput();
    }
    
    private void HandleInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        // Activate/Deactivate Pause Menu
        if (!pauseScreen.activeInHierarchy && !optionsScreen.activeInHierarchy && !guideScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            GameManager.Instance.GameIsPaused = true;
        }
        else if (pauseScreen.activeInHierarchy && !optionsScreen.activeInHierarchy && !guideScreen.activeInHierarchy)
        {
            Continue();
        }
    }
    
    private void Continue()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        GameManager.Instance.GameIsPaused = false;
    }
    
    private void Guide()
    {
        pauseScreen.SetActive(false);
        guideScreen.SetActive(true);
    }
    
    private void Options()
    {
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }
    
    private void Return()
    {
        // Save current progress
        SaveData saveData = new SaveData {currentScene = SceneManager.GetActiveScene().buildIndex};
        SaveManager.Save(saveData);
        
        // Return to main menu
        SceneManager.LoadScene("MainMenu");
    }
    
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}