public class CookingAI : AISystem
{
    private OrderManager orderManager;
    private Order currentOrder;
    
    // Decision Settings
    private int sIndex;

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
        HandleStates();
        
        switch (sIndex)
        {
            case 0: // Cook
            {
                SetState(new CookState(this, currentOrder));
                break;
            }
            case 1: // Random Position
            {
                SetState(new RandomPositionState(this, CurrentZone));
                break;
            }
        }
    }
    
    private void HandleStates()
    {
        // Check if there is a new order to cook (if no order is being prepared)
        if (orderManager.HasOrders && !IsCooking)
        {
            currentOrder = orderManager.GetOrder();
            IsCooking = true;
        }
        
        if (IsCooking) sIndex = 0;
        else sIndex = 1;
    }
}
