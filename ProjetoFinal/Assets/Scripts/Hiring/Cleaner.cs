using UnityEngine;

[CreateAssetMenu(fileName = "Cleaner", menuName = "ScriptableObjects/Cleaner", order = 4)]
public class Cleaner : ScriptableObject
{
    [SerializeField] private string cleanerID;
    [SerializeField] private int carryingCapacity;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int wage;

    public string CleanerID => cleanerID;
    public int CarryingCapacity => carryingCapacity;
    public float MovementSpeed => movementSpeed;
    public int Wage => wage;
}