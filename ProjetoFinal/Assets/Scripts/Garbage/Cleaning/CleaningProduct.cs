using UnityEngine;

[CreateAssetMenu(fileName = "CleaningProduct", menuName = "ScriptableObjects/CleaningProduct", order = 3)]
public class CleaningProduct : ScriptableObject
{
    [SerializeField] private string productID;
    [SerializeField] private float timeBoost;
    [SerializeField] private int cost;
    [SerializeField] private int numberOfUses;
    [SerializeField] private bool unlimited;
    
    public string ProductID => productID;
    public float TimeBoost => timeBoost;
    public int Cost => cost;
    public int NumberOfUses => numberOfUses;
    public bool Unlimited => unlimited;
}