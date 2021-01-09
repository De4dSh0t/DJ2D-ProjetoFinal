using UnityEngine;

public class Garbage : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float cleaningTime;
    
    [Header("Currency Settings")]
    [SerializeField] private int cleaningReward;
    
    public Zone Zone { get; set; }
    public Vector3Int Position { get; set; }
    public float CleaningTime => cleaningTime;
    public int CleaningReward => cleaningReward;
}
