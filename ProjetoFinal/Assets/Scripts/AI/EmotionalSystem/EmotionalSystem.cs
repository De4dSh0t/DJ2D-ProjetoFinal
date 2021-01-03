using UnityEngine;

public class EmotionalSystem : MonoBehaviour
{
    [Header("Max Value Settings")]
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxEmotion;
    private float energyMeter;
    private float emotionMeter;
    
    [Header("Energy Level Settings")]
    [SerializeField] private float level1 = .5f;
    [SerializeField] private float level2 = -.5f;
    [SerializeField] private float level3 = -1;
    private float energyLevel;
    
    [Header("Emotion Level Settings")]
    [Tooltip("X: Min Energy | Y: Max Energy ")] [SerializeField] private Vector2 happy;
    [Tooltip("X: Min Energy | Y: Max Energy ")] [SerializeField] private Vector2 normal;
    [Tooltip("X: Min Energy | Y: Max Energy ")] [SerializeField] private Vector2 angry;

    // Emotional States Settings
    private EmotionalStates emotionalState;
    
    public EmotionalStates EmotionalState => emotionalState;
    
    void Awake()
    {
        energyMeter = maxEnergy;
        emotionMeter = maxEmotion;
        energyLevel = maxEnergy / 3;
    }
    
    void Update()
    {
        // Energy level increases 0.5 per second
        UpdateEnergy(0.5f * Time.deltaTime);
        
        // Energy Level 1 (Emotion: -0.5 per second)
        if (energyMeter > energyLevel * 2) UpdateEmotion(level1 * Time.deltaTime);
        
        // Energy Level 2 (Emotion: -1 per second)
        if (energyMeter > energyLevel && energyMeter <= energyLevel * 2) UpdateEmotion(level2 * Time.deltaTime);
        
        // Energy Level 3 (Emotion: -3 per second)
        if (energyMeter <= energyLevel) UpdateEmotion(level3 * Time.deltaTime);
    }
    
    public void UpdateEnergy(float value)
    {
        energyMeter = Mathf.Clamp(energyMeter + value, 0, maxEnergy);
    }

    public float GetEnergy()
    {
        return energyMeter;
    }
    
    private void UpdateEmotion(float value)
    {
        emotionMeter = Mathf.Clamp(emotionMeter + value, 0, maxEmotion);
        
        // Happy (80 - 100)
        if (emotionMeter >= happy.x && emotionMeter <= happy.y) emotionalState = EmotionalStates.Happy;
        
        // Normal (40 - 79)
        if (emotionMeter >= normal.x && emotionMeter < normal.y) emotionalState = EmotionalStates.Normal;
        
        //Angry (0 - 39)
        if (emotionMeter >= angry.x && emotionMeter < angry.y) emotionalState = EmotionalStates.Angry;
    }
}