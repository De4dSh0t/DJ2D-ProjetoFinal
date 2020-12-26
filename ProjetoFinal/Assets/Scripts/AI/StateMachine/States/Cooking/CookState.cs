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
        yield return new WaitForSeconds(order.food.cookingTime);
        
        // Remove order from order list
        aiSystem.OrderManager.RemoveOrder(order);
        
        aiSystem.SetState(new DeliverState(aiSystem, order.guest, aiSystem.CurrentRoom));
    }
}
