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

    public void UpdateScore(int playerScore, int enemyScore)
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();

        scorePopupText.text = ($"{playerScore}-{enemyScore}");
        scorePopupText.gameObject.SetActive(true);

        StartCoroutine(HidePopupAfterDelay());
    }

    private IEnumerator HidePopupAfterDelay()
    {
        yield return new WaitForSeconds(delayDuration);
        scorePopupText.gameObject.SetActive(false);
    }
}
