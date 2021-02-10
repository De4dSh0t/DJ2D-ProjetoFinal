using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text clock;
    
    [SerializeField] private float levelTime;
    [SerializeField] private int startsAt = 9;
    [SerializeField] private int endsAt = 21;
    private float elapsedTime;
    private float convertedTime;
    private bool ended;
    private float min;
    
    public float CurrentHour { get; private set; }
    
    private void Start()
    {
        HandleTimeConversion();
        CurrentHour = startsAt;
        UpdateDisplay();
    }
    
    private void Update()
    {
        // Wait until the start animation plays
        if (Time.time <= 3) return;
        
        HandleElapsedTime();
    }
    
    private void HandleTimeConversion()
    {
        int hours = Mathf.Abs(endsAt - startsAt);
        convertedTime = levelTime / hours;
    }
    
    private void HandleElapsedTime()
    {
        if (ended) return;
        
        CurrentHour += 60 / (convertedTime * 60) * Time.deltaTime;
        if (min >= 60) min = 0;
        min += 60 / convertedTime * Time.deltaTime;
        
        UpdateDisplay();
        HandleEnd();
    }
    
    private void HandleEnd()
    {
        if (CurrentHour < endsAt) return;
        
        GameManager.Instance.EndLevel();
        ended = true;
    }
    
    private void UpdateDisplay()
    {
        DateTime.TryParse($"{(int) CurrentHour}:{(int) min}", out var dt);
        clock.text = $"{dt:HH:mm}";
    }
}