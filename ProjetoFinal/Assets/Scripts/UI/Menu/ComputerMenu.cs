using UnityEngine;
using UnityEngine.UI;

public class ComputerMenu : MonoBehaviour
{
    [Header("Button Settings")] 
    [SerializeField] private Button hiringButton;
    [SerializeField] private Button dismissButton;
    [SerializeField] private Button productsButton;
    [SerializeField] private Button closeButton;

    [Header("Screen Settings")] 
    [SerializeField] private GameObject hiringMenu;
    [SerializeField] private GameObject dismissingMenu;
    [SerializeField] private GameObject productsMenu;
    
    public void Start()
    {
        hiringButton.onClick.AddListener(() => ChangeScreen(hiringMenu));
        dismissButton.onClick.AddListener(() => ChangeScreen(dismissingMenu));
        productsButton.onClick.AddListener(() => ChangeScreen(productsMenu));
        closeButton.onClick.AddListener(Close);
    }
    
    private void ChangeScreen(GameObject screen)
    {
        gameObject.SetActive(false);
        screen.SetActive(true);
    }
    
    private void Close()
    {
        gameObject.SetActive(false);
    }
}