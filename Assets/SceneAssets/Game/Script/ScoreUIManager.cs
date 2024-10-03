using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI playerScoreText;
    [SerializeField]TextMeshProUGUI enemyScoreText;

    public void UpdateScore(int playerScore, int enemyScore)
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
    }
}
