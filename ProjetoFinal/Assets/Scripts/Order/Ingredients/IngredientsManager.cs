using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    [Header("Ingredients Settings")]
    [SerializeField] private Ingredient[] startingIngredients;
    private readonly Dictionary<IngredientTypes, int> availableIngredients = new Dictionary<IngredientTypes, int>();

    public Dictionary<IngredientTypes, int> AvailableIngredients => availableIngredients;

    private void Start()
    {
        // Add starting ingredients to the dictionary
        foreach (var ingredient in startingIngredients)
        {
            availableIngredients.Add(ingredient.IngredientType, ingredient.Quantity);
        }
    }

    public int GetQuantityByType(IngredientTypes type)
    {
        return availableIngredients[type];
    }

    public void AddIngredient(IngredientTypes ingredientType, int quantity)
    {
        if (!availableIngredients.ContainsKey(ingredientType)) return;
        availableIngredients[ingredientType] += quantity;
    }

    public void RemoveIngredient(IngredientTypes ingredientType, int quantity)
    {
        if (!availableIngredients.ContainsKey(ingredientType)) return;
        
        if (availableIngredients[ingredientType] < quantity) 
        { 
            availableIngredients[ingredientType] = 0;
        }
        else
        {
            availableIngredients[ingredientType] -= quantity;
        }
    }
}