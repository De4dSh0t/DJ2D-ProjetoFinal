using TMPro;
using UnityEngine;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text numUses;
    [SerializeField] private TMP_Text price;
    
    public void Setup(string id, int nUses, int cost)
    {
        title.text = id;
        numUses.text = nUses.ToString();
        price.text = cost.ToString();
    }
}