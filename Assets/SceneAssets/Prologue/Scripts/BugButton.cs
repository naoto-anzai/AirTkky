using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugButton : MonoBehaviour
{

    public void LoadIntroScene()
    {
        SceneManager.LoadSceneAsync("TitleBug");
    }
}
