using UnityEngine;

public class Garbage : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float cleaningTime;
    
    [Header("Product Settings")]
    [SerializeField] private CleaningProduct requiredProduct;
    
    public Zone Zone { get; set; }
    public Vector3Int Position { get; set; }
    public float CleaningTime => cleaningTime;
    public CleaningProduct RequiredProduct => requiredProduct;
}
