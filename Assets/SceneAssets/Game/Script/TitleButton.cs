using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Game");
    }
}