using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private Food[] foodList;
    private readonly List<Order> orders = new List<Order>();

    public Food[] FoodList => foodList;

    /// <summary>
    /// Used to check if there are new orders
    /// </summary>
    public bool HasOrders { get; private set; }

    public void Update()
    {
        if (orders.Count > 0 && !HasOrders) HasOrders = true;
        if (orders.Count == 0 && HasOrders) HasOrders = false;
    }

    public Order GetOrder()
    {
        if (!HasOrders) return null;
        return orders[0];
    }

    public void AddOrder(Order order)
    {
        orders.Add(order);
    }

    public void RemoveOrder(Order order)
    {
        orders.Remove(order);
    }
}
