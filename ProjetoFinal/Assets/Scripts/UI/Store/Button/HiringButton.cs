using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HiringButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text carryingCapacity;
    [SerializeField] private TMP_Text movementSpeed;
    [SerializeField] private TMP_Text price;
    private Cleaner cleanerInfo;
    
    public event Action<Cleaner> OnSelect;
    
    public void Setup(Cleaner cleaner)
    {
        title.text = cleaner.CleanerID;
        carryingCapacity.text = cleaner.CarryingCapacity.ToString();
        movementSpeed.text = cleaner.MovementSpeed.ToString();
        price.text = cleaner.Wage.ToString();
        cleanerInfo = cleaner;
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke(cleanerInfo);
    }
}