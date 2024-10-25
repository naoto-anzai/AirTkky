using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using GameStates;

public class SceneLoadManager_test : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ToPrologueSequenser() 
    { 
        yield return StartCoroutine(SceneLoader(scenenames_test.prologue_test));
    }


    public IEnumerator ResultLoader(gameresults isWin) 
    { 
        if (isWin == gameresults.win)
        {
            yield return SceneLoader(scenenames_test.endingwin_test);
        }
        else if (isWin == gameresults.lose)
        {
            yield return SceneLoader(scenenames_test.endinglose_test);
        }
    }
        
    
    public IEnumerator SceneLoader(string sceneName)
    {
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;  
        }

    }
}
