using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Carrying Settings")]
    [SerializeField] private int maxCarryingCapacity;
    private int carryingCount;
    
    /// <summary>
    /// Current Carrying Count
    /// </summary>
    public int CarryingCount
    {
        get => carryingCount;
        set
        {
            if (carryingCount + value >= maxCarryingCapacity)
            {
                carryingCount = maxCarryingCapacity;
                return;
            }
            
            carryingCount += value;
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}