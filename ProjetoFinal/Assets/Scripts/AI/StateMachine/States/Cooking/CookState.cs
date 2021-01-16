using System.Collections;
using UnityEngine;

public class CookState : IState
{
    private readonly CookingAI aiSystem;
    private readonly Order order;
    
    public CookState(CookingAI system, Order order)
    {
        aiSystem = system;
        this.order = order;
    }
    
    public IEnumerator Execute()
    {
        // Search Ingredients
        if (HasIngredients())
        {
            // Remove necessary ingredients from stock
            UseIngredients();
        }
        else
        {
            // Cannot cook
            aiSystem.SetState(new IdleState(aiSystem));
            yield break;
        }
        
        // Go to the kitchen
        yield return aiSystem.SetState(new MoveState(aiSystem, aiSystem.Pathfinding, aiSystem.PositionInt, aiSystem.SearchActionZone("Cook").Waypoint));
        aiSystem.Animator.SetBool("isWalking", false);
        
        // Cook (normal food cooking time)
        yield return new WaitForSeconds(order.food.CookingTime);
        aiSystem.Animator.SetBool("isWalking", true);
        
        // Remove order from order list
        aiSystem.OrderManager.RemoveOrder(order);
        
        aiSystem.SetState(new DeliverState(aiSystem, order.guest, order.food));
    }

    private bool HasIngredients()
    {
        Ingredient[] necessaryIngredients = order.food.Ingredients;
        
        foreach (var ingredient in necessaryIngredients)
        {
            // Check if there aren't enough ingredients in stock to prepare the pretended food
            if (aiSystem.IngredientsManager.GetQuantityByType(ingredient.IngredientType) < ingredient.Quantity)
            {
                Debug.Log($"There is no more { ingredient.IngredientType }");
                return false;
            }
        }
        
        return true;
    }
    
    private void UseIngredients()
    {
        Ingredient[] ingredients = order.food.Ingredients;
        
        foreach (var ingredient in ingredients)
        {
            aiSystem.IngredientsManager.RemoveIngredient(ingredient.IngredientType, ingredient.Quantity);
        }
    }
}
