using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/Food", order = 2)]
public class Food : ScriptableObject
{
    [SerializeField] private float cookingTime;
    [SerializeField] private Ingredient[] ingredients;

    public float CookingTime => cookingTime;
    public Ingredient[] Ingredients => ingredients;
}