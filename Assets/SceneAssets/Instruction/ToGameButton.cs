using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToGameButton : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(scenenames.game);
    }
}
