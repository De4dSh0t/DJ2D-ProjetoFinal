public struct CleanerInfo
{
    public string CleanerID { get; private set; }
    public int CarryingCapacity { get; private set; }
    public float MovementSpeed { get; private set; }
    public int Wage { get; private set; }
    
    public CleanerInfo(string id, int capacity, float speed, int wage)
    {
        CleanerID = id;
        CarryingCapacity = capacity;
        MovementSpeed = speed;
        Wage = wage;
    }
}