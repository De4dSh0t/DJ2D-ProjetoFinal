using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    // Timer Settings
    private float startingTime;
    
    void Start()
    {
        startingTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startingTime <= 2) return;

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) 
            || Time.time - startingTime >= 16)
        {
            SaveManager.DeleteSave();
            SceneManager.LoadScene("MainMenu");
        }
    }
}