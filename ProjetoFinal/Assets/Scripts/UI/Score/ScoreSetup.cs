using TMPro;
using UnityEngine;

public class ScoreSetup : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text requiredPoints;
    [SerializeField] private TMP_Text playerPoints;
    
    public void Setup(float requiredP, float playerP)
    {
        requiredPoints.text = requiredP.ToString();
        playerPoints.text = playerP.ToString();
    }
}