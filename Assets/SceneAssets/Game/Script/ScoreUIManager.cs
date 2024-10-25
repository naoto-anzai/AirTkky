using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameStates;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI enemyScoreText;

    [SerializeField] float delayDuration;
    [SerializeField] TextMeshProUGUI scorePopupText;

    private gameresults isWin;

    [SerializeField] SceneLoadManager sceneLoadManager;

    [SerializeField] GameObject cotacky;
    [SerializeField] Sprite cotackyWin;
    [SerializeField] Sprite cotackyLose;

    private void Awake()
    {
        if (scorePopupText == null)
        {
            scorePopupText = GameObject.Find("ScorePopup").GetComponent<TextMeshProUGUI>();
        }
        if (scorePopupText == null)
        {
            sceneLoadManager = GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>();
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
                isWin = gameresults.win;
                if(enemyScore == 0)
                {
                    isWin = gameresults.special;
                }
                cotacky.GetComponent<SpriteRenderer>().sprite = cotackyLose;
            }
            else
            {
                isWin = gameresults.lose;
                scorePopupText.text = ("YOU LOSE");
                cotacky.GetComponent<SpriteRenderer>().sprite = cotackyWin;
            }
            yield return new WaitForSeconds(delayDuration);
            sceneLoadManager.ToEndings(isWin);
        }

        scorePopupText.gameObject.SetActive(false);
    }
}
