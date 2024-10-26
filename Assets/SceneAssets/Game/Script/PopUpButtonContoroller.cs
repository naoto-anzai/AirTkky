using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using GameStates;

public class PopUpButtonContoroller : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject popUp;

    [SerializeField] pack_game_manager packGameManager;

    private void Start()
    {
        popUp.SetActive(true);
        button.onClick.AddListener(OnClick);
        inputField = inputField.GetComponent<TMP_InputField>();
    }

    public void OnClick()
    {
        string text = inputField.text;
        if (text == "7777")
        {
            packGameManager.StartRalley(players.player);
            popUp.SetActive(false);
        }
    }
}
