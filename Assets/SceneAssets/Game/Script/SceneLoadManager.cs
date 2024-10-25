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



    public IEnumerator GameResultLoader(gameresults isWin) 
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
        
    // 任意のシーンをロードする関数
    public IEnumerator SceneLoader(string sceneName)
    {
        // 非同期でシーンをロードする
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // ロードが完了するまで待機
        while (!asyncLoad.isDone)
        {
            // ロードの進行状況を確認してUIに反映させることもできる
            UnityEngine.Debug.Log(asyncLoad.progress);
            yield return null;  // 次のフレームまで待つ
        }

    }
}
