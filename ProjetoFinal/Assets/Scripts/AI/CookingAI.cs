using UnityEngine;

public class CookingAI : AISystem
{
    private OrderManager orderManager;
    private Order currentOrder;

    public bool IsCooking { get; set; }

    /// <summary>
    /// Returns OrderManager reference to interact with the order list
    /// </summary>
    public OrderManager OrderManager => orderManager;
    
    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();
        
        DecisionMaking();
    }

    public override void DecisionMaking()
    {
        // Check if there is a new order to cook (if no order is being prepared)
        if (orderManager.HasOrders && !IsCooking)
        {
            currentOrder = orderManager.GetOrder();
            IsCooking = true;
        }

        if (IsCooking) SetState(new CookState(this, currentOrder));
        else SetState(new RandomPositionState(this, CurrentRoom));
    }
}
