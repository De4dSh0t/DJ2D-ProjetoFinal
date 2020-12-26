using System.Collections;
using System.Collections.Generic;
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
        aiSystem.OrderManager.AddOrder(ChooseRandomFood(aiSystem.FoodList));
        aiSystem.SetState(new IdleState(aiSystem));
        yield break;
    }

    private Order ChooseRandomFood(List<Food> fList)
    {
        return new Order(aiSystem, fList[Random.Range(0, fList.Count)]);
    }
}
