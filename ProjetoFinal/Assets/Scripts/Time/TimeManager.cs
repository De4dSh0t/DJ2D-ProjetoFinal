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
        if (elapsedTime >= rate)
        {
            CurrentHour++;
            rate += convertedTime;
            print(CurrentHour);
        }
        elapsedTime += Time.deltaTime;
    }
    
    private void HandleTimeConversion()
    {
        int hours = Mathf.Abs(endsAt - startsAt);
        convertedTime = levelTime / hours;
        rate = convertedTime;
    }
}