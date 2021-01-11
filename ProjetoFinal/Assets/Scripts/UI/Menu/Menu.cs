using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] protected Button closeButton;
    [SerializeField] protected GameObject computerScreen;

    protected void OnEnable()
    {
        ClearList();
        DisplayList();
        
        closeButton.onClick.AddListener(Close);
    }

    protected void ClearList()
    {
        foreach (var button in spawnedButtons)
        {
            Destroy(button);
        }
    }
    
    protected void Close()
    {
        gameObject.SetActive(false);
        computerScreen.SetActive(true);
    }
    
    protected virtual void DisplayList() {}
    protected virtual void ShowPrompt() {}
    protected virtual void ShowPrompt(T entity) {}
    protected virtual void ClosePrompt() {}
    public virtual void Buy() {}
    public virtual void Cancel() {}
}