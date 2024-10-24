using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI enemyScoreText;

    [SerializeField] float delayDuration;
    [SerializeField] TextMeshProUGUI scorePopupText;

    private void Awake()
    {
        if (scorePopupText == null)
        {
            scorePopupText = GameObject.Find("ScorePopup").GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        scorePopupText.gameObject.SetActive(false);
    }

    public IEnumerator UpdateScore(int playerScore, int enemyScore)
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();

        scorePopupText.text = ($"{playerScore}-{enemyScore}");
        scorePopupText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delayDuration);

        if (playerScore == 7 || enemyScore == 7)
        {
            if (playerScore == 7)
            {
                scorePopupText.text = ("YOU WIN");
            }
            else
            {
                scorePopupText.text = ("YOU LOSE");
            }
            yield return new WaitForSeconds(delayDuration);
        }

        scorePopupText.gameObject.SetActive(false);
    }
}
