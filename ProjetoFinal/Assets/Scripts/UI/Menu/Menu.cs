using System.Collections.Generic;
using UnityEngine;

public abstract class Menu<T> : MonoBehaviour
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
        ClearList();
        DisplayList();
    }

    protected void ClearList()
    {
        foreach (var button in spawnedButtons)
        {
            Destroy(button);
        }
    }
    protected virtual void DisplayList() {}
    protected virtual void ShowPrompt() {}
    protected virtual void ShowPrompt(T entity) {}
    protected virtual void ClosePrompt() {}
    public virtual void Buy() {}
    public virtual void Cancel() {}
}