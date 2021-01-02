using UnityEngine;

[System.Serializable]
public struct Ingredient
{
    [SerializeField] private IngredientTypes ingredientType;
    [SerializeField] private int quantity;

    public IngredientTypes IngredientType => ingredientType;
    public int Quantity => quantity;
}