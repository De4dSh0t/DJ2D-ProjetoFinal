using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DismissPopUp : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text perMinute;
    [SerializeField] private TMP_Text total;
    [SerializeField] private TMP_Text capacity;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private Button dismiss;
    [SerializeField] private Button close;
    
    [Header("Manager Settings")]
    [SerializeField] private CleanerManager cleanerManager;
    private GameObject selectedCleaner;
    
    private void Start()
    {
        dismiss.onClick.AddListener(Dismiss);
        close.onClick.AddListener(Close);
    }
    
    public void Setup(GameObject gameObj, CleanerInfo cleanerInfo)
    {
        title.text = cleanerInfo.CleanerID;
        capacity.text = cleanerInfo.CarryingCapacity.ToString();
        speed.text = cleanerInfo.MovementSpeed.ToString();
        selectedCleaner = gameObj;
    }
    
    private void Dismiss()
    {
        cleanerManager.DismissCleaner(selectedCleaner);
    }
    
    private void Close()
    {
        gameObject.SetActive(false);
    }
}