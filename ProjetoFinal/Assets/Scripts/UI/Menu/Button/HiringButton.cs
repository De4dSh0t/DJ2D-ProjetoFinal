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
    public CleanerInfo cleanerInfo;
    
    public event Action<CleanerInfo> OnSelect;
    
    public void Setup(CleanerInfo cleanerInfo)
    {
        title.text = cleanerInfo.CleanerID;
        carryingCapacity.text = cleanerInfo.CarryingCapacity.ToString();
        movementSpeed.text = cleanerInfo.MovementSpeed.ToString();
        price.text = cleanerInfo.Wage.ToString();
        this.cleanerInfo = cleanerInfo;
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke(cleanerInfo);
    }
}