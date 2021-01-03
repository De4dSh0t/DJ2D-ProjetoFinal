using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/Food", order = 2)]
public class Food : ScriptableObject
{
    [SerializeField] private float cookingTime;
    [SerializeField] private float energyToPrepare;
    [SerializeField] private Ingredient[] ingredients;

    public float CookingTime => cookingTime;
    public float EnergyToPrepare => energyToPrepare;
    public Ingredient[] Ingredients => ingredients;
}