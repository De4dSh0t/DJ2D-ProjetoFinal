using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("AudioManager");
                obj.AddComponent<GameManager>();
            }
                
            return instance;
        }
    }
    
    [Header("Screen Settings")]
    [SerializeField] private GameObject scoreScreen;
    
    public bool GameIsPaused { get; set; }
    
    public event Action OnLevelEnd;
    
    private void Awake()
    {
        instance = this;
    }
    
    public void EndLevel()
    {
        // Trigger event
        OnLevelEnd?.Invoke();
        
        scoreScreen.SetActive(true);
    }
}