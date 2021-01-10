using System.Collections.Generic;
using UnityEngine;

public abstract class Store<T> : MonoBehaviour
{
    [Header("Products Settings")]
    [SerializeField] protected List<T> element;
    
    [Header("Prefab Settings")]
    [SerializeField] protected GameObject elementPrefab;
    
    [Header("UI Settings")]
    [SerializeField] protected GameObject content;
    [SerializeField] protected GameObject prompt;

    protected void Start()
    {
        DisplayList();
    }

    protected virtual void DisplayList() {}
    protected virtual void ShowPrompt() {}
    protected virtual void ClosePrompt() {}
    public virtual void Buy() {}
    public virtual void Cancel() {}
}