using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float levelTime;
    [SerializeField] private int startsAt = 9;
    [SerializeField] private int endsAt = 21;
    private float elapsedTime;
    private float convertedTime;
    private float rate;
    
    public int CurrentHour { get; private set; }
    
    private void Start()
    {
        HandleTimeConversion();
        CurrentHour = startsAt;
    }
    
    private void Update()
    {
        HandleElapsedTime();
    }
    
    private void HandleTimeConversion()
    {
        int hours = Mathf.Abs(endsAt - startsAt);
        convertedTime = levelTime / hours;
        rate = convertedTime;
    }
    
    private void HandleElapsedTime()
    {
        if (elapsedTime >= rate)
        {
            CurrentHour++;
            rate += convertedTime;
            print(CurrentHour);
            
            // Check if time reached "endsAt"
            HandleEnd();
        }
        elapsedTime += Time.deltaTime;
    }
    
    private void HandleEnd()
    {
        if (CurrentHour == endsAt) GameManager.Instance.EndLevel();
    }
}