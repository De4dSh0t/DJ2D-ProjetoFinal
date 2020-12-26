public class Order
{
    public readonly GuestAI guest;
    public readonly Food food;
    
    public Order(GuestAI referenceAI, Food foodToCook)
    {
        guest = referenceAI;
        food = foodToCook;
    }
}
