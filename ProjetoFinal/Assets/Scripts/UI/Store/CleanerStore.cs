using UnityEngine;

public class CleanerStore : Store<Cleaner>
{
    private void Start()
    {
        DisplayList();
    }
    
    protected override void DisplayList()
    {
        foreach (var cleaner in element)
        {
            GameObject button = Instantiate(elementPrefab, content.transform);
            button.GetComponent<HiringButton>().Setup(cleaner.CleanerID, cleaner.CarryingCapacity, cleaner.MovementSpeed, cleaner.Wage);
        }
    }
}