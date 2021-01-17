using UnityEngine;
using UnityEngine.UI;

public class CleaningMeter : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset;
    
    private void Update()
    {
        UpdatePos();
    }
    
    private void UpdatePos()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
    
    public void SetMeter(float val, float max)
    {
        slider.maxValue = max;
        slider.value = val;
    }
    
    public void SetActive(bool active)
    {
        slider.gameObject.SetActive(active);
    }
}