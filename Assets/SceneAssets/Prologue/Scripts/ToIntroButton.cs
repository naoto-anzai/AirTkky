using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToIntroButton : MonoBehaviour
{
    [SerializeField] ButtonSEManager buttonSEManager;

    public void LoadIntroScene()
    {
        buttonSEManager = ButtonSEManager.Instance;
        buttonSEManager.PlayWhenClick();
        SceneManager.LoadSceneAsync(scenenames.intro);
    }
}
