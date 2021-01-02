using System.Collections;
using UnityEngine;

public class OrderState : IState
{
    private readonly GuestAI aiSystem;

    public OrderState(GuestAI system)
    {
        aiSystem = system;
    }
    
    public IEnumerator Execute()
    {
        aiSystem.OrderManager.AddOrder(ChooseRandomFood(aiSystem.OrderManager.FoodList));
        aiSystem.SetState(new IdleState(aiSystem));
        yield break;
    }

    private Order ChooseRandomFood(Food[] fList)
    {
        return new Order(aiSystem, fList[Random.Range(0, fList.Length)]);
    }
}
