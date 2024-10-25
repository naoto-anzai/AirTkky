using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToIntroButton : MonoBehaviour
{
    public void LoadIntroScene()
    {
        SceneManager.LoadSceneAsync(scenenames.intro);
    }
}
