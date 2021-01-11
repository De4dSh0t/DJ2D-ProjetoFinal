using UnityEngine;

[CreateAssetMenu(fileName = "CleaningProduct", menuName = "ScriptableObjects/CleaningProduct", order = 3)]
public class CleaningProduct : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private string productID;
    [SerializeField] private float timeBoost;
    [SerializeField] private int cost;
    [SerializeField] private int numberOfUses;
    [SerializeField] private bool unlimited;

    public Sprite Sprite => sprite;
    public string ProductID => productID;
    public float TimeBoost => timeBoost;
    public int Cost => cost;
    public int NumberOfUses => numberOfUses;
    public bool Unlimited => unlimited;
}