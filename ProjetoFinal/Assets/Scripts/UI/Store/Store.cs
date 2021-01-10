using System.Collections.Generic;
using UnityEngine;

public abstract class Store<T> : MonoBehaviour
{
    [Header("Currency Settings")]
    [SerializeField] protected CurrencyManager currencyManager;
    
    [Header("Element Settings")]
    [SerializeField] protected List<T> element;
    protected readonly List<GameObject> spawnedButtons = new List<GameObject>();
    
    [Header("Prefab Settings")]
    [SerializeField] protected GameObject elementPrefab;
    
    [Header("UI Settings")]
    [SerializeField] protected GameObject content;
    [SerializeField] protected GameObject prompt;

    protected void OnEnable()
    {
        foreach (var button in spawnedButtons)
        {
            Destroy(button);
        }
        
        DisplayList();
    }

    protected virtual void DisplayList() {}
    protected virtual void ShowPrompt() {}
    protected virtual void ClosePrompt() {}
    public virtual void Buy() {}
    public virtual void Cancel() {}
}