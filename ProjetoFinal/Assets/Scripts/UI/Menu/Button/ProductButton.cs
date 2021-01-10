using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text numUses;
    [SerializeField] private TMP_Text price;
    
    public UnityEvent OnSelect;
    
    public void Setup(string id, int nUses, int cost)
    {
        title.text = id;
        numUses.text = nUses.ToString();
        price.text = cost.ToString();
    }
    
    public void Pressed()
    {
        OnSelect?.Invoke();
    }
}