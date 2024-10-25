using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToTitleButton : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneManager.LoadSceneAsync(scenenames.title);
    }
}
