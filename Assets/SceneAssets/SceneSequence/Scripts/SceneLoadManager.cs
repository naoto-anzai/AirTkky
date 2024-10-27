using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using GameStates;

public class SceneLoadManager : MonoBehaviour
{

    public IEnumerator ToPrologueSequenser() 
    { 
        yield return StartCoroutine(SceneLoader(scenenames_test.prologue_test));
    }


    public IEnumerator ToResultSequenser() 
    {     
        yield return StartCoroutine(SceneLoader(scenenames_test.result_test));
    }
        
    
    public IEnumerator SceneLoader(string sceneName)
    {
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;  
        }

    }
    public void ToEndings(gameresults isWin)
    {
        if(isWin == gameresults.win)
        {
            SceneManager.LoadSceneAsync(scenenames.endingwin);
        }
        if(isWin == gameresults.lose)
        {
            SceneManager.LoadSceneAsync(scenenames.endinglose);
        }
        if(isWin == gameresults.special)
        {
            SceneManager.LoadSceneAsync(scenenames.endingspecial);
        }
    }

    public void ToTrueEndings(gameresults isWin)
    {
        if (isWin == gameresults.win)
        {
            SceneManager.LoadSceneAsync("EndingBugWin");
        }
        if (isWin == gameresults.lose)
        {
            SceneManager.LoadSceneAsync("EndingBugLose");
        }
        if (isWin == gameresults.special)
        {
            SceneManager.LoadSceneAsync(scenenames.endingspecial);
        }
    }
}
