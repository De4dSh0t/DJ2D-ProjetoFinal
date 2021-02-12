using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string nextScene;
    [SerializeField] private float timeToTransit;
    private float timer;
    
    void Start()
    {
        timer = timeToTransit;
    }
    
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}