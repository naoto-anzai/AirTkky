using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToTitleButton : MonoBehaviour
{
    [SerializeField] CreditsAudioManager audioManager;

    public void LoadTitleScene()
    {
        audioManager = CreditsAudioManager.Instance;
        audioManager.PlayWhenClick();
        SceneManager.LoadSceneAsync(scenenames.title);
    }
}
