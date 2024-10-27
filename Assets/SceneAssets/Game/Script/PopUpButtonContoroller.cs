using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using GameStates;
using PackScoreManager;

public class PopUpButtonContoroller : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject popUp;

    [SerializeField] pack_game_manager packGameManager;
    [SerializeField] pack_score_manager packScoreManager;

    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI enemyScoreText;

    int playerScore, enemyScore;

    private void Start()
    {
        popUp.SetActive(true);
        button.onClick.AddListener(OnClick);
        inputField = inputField.GetComponent<TMP_InputField>();

        //EscapeÇâüÇµÇΩèuä‘ÇÃÉXÉRÉAÇÃèÛãµÇì«Ç›çûÇﬁ
        playerScore = packScoreManager.score_player;
        enemyScore = packScoreManager.score_enemy;
    }

    public void OnClick()
    {
        string text = inputField.text;
        if (text == "7777")
        {
            packGameManager.StartRalley(players.player);

            playerScoreText.text = playerScore.ToString();
            enemyScoreText.text = enemyScore.ToString();

            packScoreManager.score_player = playerScore;
            packScoreManager.score_enemy = enemyScore;
            popUp.SetActive(false);
        }
    }
}
