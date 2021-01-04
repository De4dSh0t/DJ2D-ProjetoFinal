public class CookingAI : AISystem
{
    // Order Settings
    private OrderManager orderManager;
    private Order currentOrder;

    // Ingredients Settings
    private IngredientsManager ingredientsManager;
    
    // Decision Settings
    private int sIndex;

    public bool IsCooking { get; set; }

    /// <summary>
    /// Returns OrderManager reference to interact with the order list
    /// </summary>
    public OrderManager OrderManager => orderManager;

    /// <summary>
    /// Returns IngredientsManager reference to interact with available ingredients list
    /// </summary>
    public IngredientsManager IngredientsManager => ingredientsManager;

    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();
        ingredientsManager = FindObjectOfType<IngredientsManager>();
        
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
