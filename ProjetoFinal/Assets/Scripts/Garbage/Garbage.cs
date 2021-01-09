using UnityEngine;

public class Garbage : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float cleaningTime;
    
    [Header("Product Settings")]
    [SerializeField] private CleaningProduct requiredProduct;
    
    [Header("Currency Settings")]
    [SerializeField] private int cleaningReward;
    
    public Zone Zone { get; set; }
    public Vector3Int Position { get; set; }
    public float CleaningTime => cleaningTime;
    public CleaningProduct RequiredProduct => requiredProduct;
    public int CleaningReward => cleaningReward;
}
