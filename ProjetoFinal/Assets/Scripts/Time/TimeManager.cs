using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float levelTime;
    [SerializeField] private int startsAt = 9;
    [SerializeField] private int endsAt = 21;
    private float elapsedTime;
    private int currentHour;
    private float convertedTime;
    private float rate;
    
    private void Start()
    {
        HandleTimeConversion();
        currentHour = startsAt;
    }
    
    private void Update()
    {
        if (elapsedTime >= rate)
        {
            currentHour++;
            rate += convertedTime;
            print(currentHour);
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