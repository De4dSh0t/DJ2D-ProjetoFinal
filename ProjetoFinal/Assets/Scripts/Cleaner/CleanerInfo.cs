public struct CleanerInfo
{
    public string CleanerID { get; }
    public int CarryingCapacity { get; }
    public float MovementSpeed { get; }
    public int Wage { get; }
    public int TotalGarbageCollected { get; set; }
    
    public CleanerInfo(string id, int capacity, float speed, int wage, int total)
    {
        CleanerID = id;
        CarryingCapacity = capacity;
        MovementSpeed = speed;
        Wage = wage;
        TotalGarbageCollected = total;
    }
}