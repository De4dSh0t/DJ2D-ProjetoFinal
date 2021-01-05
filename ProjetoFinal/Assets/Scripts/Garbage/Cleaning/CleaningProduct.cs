using UnityEngine;

[CreateAssetMenu(fileName = "CleaningProduct", menuName = "ScriptableObjects/CleaningProduct", order = 3)]
public class CleaningProduct : ScriptableObject
{
    [SerializeField] private string productID;
    [SerializeField] private int cost;
    [SerializeField] private int numberOfUses;
    
    public string ProductID => productID;
    public int Cost => cost;
    public int NumberOfUses => numberOfUses;
}