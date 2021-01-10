﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HiringButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text carryingCapacity;
    [SerializeField] private TMP_Text movementSpeed;
    [SerializeField] private TMP_Text price;
    
    public UnityEvent OnSelect;
    
    public void Setup(string id, int capacity, float speed, int wage)
    {
        title.text = id;
        carryingCapacity.text = capacity.ToString();
        movementSpeed.text = speed.ToString();
        price.text = wage.ToString();
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke();
    }
}