using System;
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
        requiredPoints.text = requiredP.ToString();
        playerPoints.text = playerP.ToString();
        state = s;
        
        if (state) stateText.text = "You did it!";
        else stateText.text = "Try again...";

        canUpdate = true;
        startingTime = Time.time;
    }
}